/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	config.language = 'fa';
	config.filebrowserImageUploadUrl = "/Admin/Common/UploadImage"
	config.extraPlugins = "codesnippet";
	config.codeSnippet_theme = "vs";
	config.contentsCss = "./ckeditor/plugins/codesnippet/lib/highlight/styles/vs.css";
	config.codeSnippet_languages = {
		cs: 'C#',
		javascript: "JavaScript"
	};
};