using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service;

public interface IJanService
{
    Task<List<JanUserViewModel>> GetUsersAsync();
}
