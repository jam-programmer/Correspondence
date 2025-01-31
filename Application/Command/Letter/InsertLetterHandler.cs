using Application.Common;
using Application.Common.Extension;
using Application.Contract;
using Application.DataTransferObject;
using Azure.Core;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Application.Command.Letter;

internal sealed class InsertLetterHandler
    : IRequestHandler<InsertLetterCommand, BaseResult>
{
    private readonly IContext _context;
    private readonly IRepository<LetterEntity> _letterRepository;
    private readonly IRepository<ReferralEntity> _referralRepository;
    private readonly IRepository<AttachmentEntity> _attachmentRepository;
    private readonly ILogger<InsertLetterHandler> _logger;
    public InsertLetterHandler
        (IRepository<LetterEntity> letterRepository,
        IRepository<AttachmentEntity> attachmentRepository,
    IRepository<ReferralEntity> referralRepository,
        IContext context,
        ILogger<InsertLetterHandler> logger)
    {
        _attachmentRepository = attachmentRepository;
        _referralRepository = referralRepository;
        _letterRepository = letterRepository;
        _logger = logger;
        _context = context;
    }
    public async Task<BaseResult> Handle
        (InsertLetterCommand request,
        CancellationToken cancellationToken)
    {
        var strategy = _context.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
          {
              using (var transaction = await _context.BeginTransactionAsync())
              {
                  try
                  {

                      LetterEntity letter = request.Letter.Adapt<LetterEntity>();

                      await _letterRepository.InsertAsync(letter, cancellationToken);

                      Task<List<ReferralEntity>> referrals =
                          AppendReferralAsync(new ReferralModel()
                          {
                              LetterId = letter.Id,
                              ReferrerCopy = request.Letter.ReferrerCopy,
                              ReferrerOriginal = request.Letter.ReferrerOriginal,
                              UserName = request.Letter.UserName
                          });

                      Task<List<AttachmentEntity>> attachments =
                      AppendAttachmentAsync(letter.Id, request.Letter.Files!);


                   


                      await Task.WhenAll(referrals, attachments);

                    await   _referralRepository.InsertAsync(referrals.Result);
                     await  _attachmentRepository.InsertAsync(attachments.Result);

                      await transaction.CommitAsync();


                      return BaseResult.Success();

                  }
                  catch (Exception ex)
                  {
                      await transaction.RollbackAsync();
                      _logger.LogError(ex, ex.Message);
                      return BaseResult.Fail(ResultType.System);
                  }
              }
          });
    }

    private async Task<List<ReferralEntity>> AppendReferralAsync
        (ReferralModel referral)
    {
        List<ReferralEntity> referrals = new();

        List<ReferrerItemModel>? Original
            = JsonConvert.DeserializeObject
            <List<ReferrerItemModel>>(referral.ReferrerOriginal);

        List<ReferrerItemModel>? Copy = JsonConvert.DeserializeObject
            <List<ReferrerItemModel>>(referral.ReferrerCopy);


        foreach (var item in Original)
        {
            ReferralEntity referralEntity = new()
            {
                UserName = item.UserName,
                FullName = item.FullName,
                Type = Domain.Enum.ReferralType.Original,
                LetterId = referral.LetterId,
                Status=Domain.Enum.StatusType.NotViewed
            };
            referrals.Add(referralEntity);
        }
        if (Copy != null && Copy.Any())
        {
            foreach (var item in Copy)
            {
                ReferralEntity referralEntity = new()
                {
                    UserName = item.UserName,
                    FullName = item.FullName,
                    Type = Domain.Enum.ReferralType.Copy,
                    LetterId = referral.LetterId,
                    Status = Domain.Enum.StatusType.NotViewed
                };
                referrals.Add(referralEntity);
            }
        }
        return referrals;
    }

    private async Task<List<AttachmentEntity>> AppendAttachmentAsync
        (Guid LetterId, List<IFormFile> files)
    {
        List<AttachmentEntity> attachments = new();
        if (files != null && files.Any())
        {
            foreach (var item in files)
            {
                AttachmentEntity attachment = new();
                attachment.LetterId = LetterId;
                attachment.Path = await item.UploadFileAsync("Attachment", null);
                attachments.Add(attachment);
            }
        }
        return attachments;
    }
}
public class ReferralModel
{
    public Guid LetterId { get; set; }
    public string? UserName { get; set; }
    public string ReferrerCopy { get; set; }
    public string ReferrerOriginal { get; set; }
}



