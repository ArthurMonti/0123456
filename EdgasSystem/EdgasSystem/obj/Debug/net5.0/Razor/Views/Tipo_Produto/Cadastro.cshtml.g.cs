#pragma checksum "C:\Users\arthu\Desktop\Estagio\Sistema\EdgasSystem\EdgasSystem\Views\Tipo_Produto\Cadastro.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "815c87269d40788915aa86a3bca528ab795b165a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tipo_Produto_Cadastro), @"mvc.1.0.view", @"/Views/Tipo_Produto/Cadastro.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\arthu\Desktop\Estagio\Sistema\EdgasSystem\EdgasSystem\Views\_ViewImports.cshtml"
using EdgasSystem;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\arthu\Desktop\Estagio\Sistema\EdgasSystem\EdgasSystem\Views\_ViewImports.cshtml"
using EdgasSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"815c87269d40788915aa86a3bca528ab795b165a", @"/Views/Tipo_Produto/Cadastro.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6400e615534c825ef542a1759ca1ee7a70616833", @"/Views/_ViewImports.cshtml")]
    public class Views_Tipo_Produto_Cadastro : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form_cadastro"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/views/Tipo_Produto/Cadastro.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\arthu\Desktop\Estagio\Sistema\EdgasSystem\EdgasSystem\Views\Tipo_Produto\Cadastro.cshtml"
  
    ViewData["Title"] = "Cadastro";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""content d-flex flex-column flex-column-fluid"" id=""kt_content"">
    <!--begin::Subheader-->
    <div class=""subheader py-2 py-lg-6 subheader-solid"" id=""kt_subheader"">
        <div class=""container-fluid d-flex align-items-center justify-content-between flex-wrap flex-sm-nowrap"">
            <!--begin::Info-->
            <div class=""d-flex align-items-center flex-wrap mr-1"">
                <!--begin::Page Heading-->
                <div class=""d-flex align-items-baseline flex-wrap mr-5"">
                    <!--begin::Page Title-->
                    <h5 class=""text-dark font-weight-bold my-1 mr-5"">Tipo de Produto</h5>
                    <!--end::Page Title-->
                    <!--begin::Breadcrumb-->
                    <ul class=""breadcrumb breadcrumb-transparent breadcrumb-dot font-weight-bold p-0 my-2 font-size-sm"">
                        <li class=""breadcrumb-item"">
                            <a");
            BeginWriteAttribute("href", " href=\"", 1041, "\"", 1048, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"text-muted\">Tipo de Produto</a>\r\n                        </li>\r\n                        <li class=\"breadcrumb-item\">\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 1205, "\"", 1212, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""text-muted"">Cadastro</a>
                        </li>

                    </ul>
                    <!--end::Breadcrumb-->
                </div>
                <!--end::Page Heading-->
            </div>
            <!--end::Info-->
            <!--begin::Toolbar-->
            <div class=""d-flex align-items-center"">
                <!--begin::Actions-->
                <a href=""#"" class=""btn btn-light-primary font-weight-bolder btn-sm"">Actions</a>
                <!--end::Actions-->
                <!--begin::Dropdown-->
                <div class=""dropdown dropdown-inline"" data-toggle=""tooltip"" title=""Quick actions"" data-placement=""left"">
                    <a href=""#"" class=""btn btn-icon"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                        <span class=""svg-icon svg-icon-success svg-icon-2x"">
                            <!--begin::Svg Icon | path:/metronic/theme/html/demo1/dist/assets/media/svg/icons/Files/File-plus.svg-->
              ");
            WriteLiteral(@"              <svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
                                <g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
                                    <polygon points=""0 0 24 0 24 24 0 24"" />
                                    <path d=""M5.85714286,2 L13.7364114,2 C14.0910962,2 14.4343066,2.12568431 14.7051108,2.35473959 L19.4686994,6.3839416 C19.8056532,6.66894833 20,7.08787823 20,7.52920201 L20,20.0833333 C20,21.8738751 19.9795521,22 18.1428571,22 L5.85714286,22 C4.02044787,22 4,21.8738751 4,20.0833333 L4,3.91666667 C4,2.12612489 4.02044787,2 5.85714286,2 Z"" fill=""#000000"" fill-rule=""nonzero"" opacity=""0.3"" />
                                    <path d=""M11,14 L9,14 C8.44771525,14 8,13.5522847 8,13 C8,12.4477153 8.44771525,12 9,12 L11,12 L11,10 C11,9.44771525 11.4477153,9 12,9 C12.5522847,9 13,9.44771525 13,10 L13,12 L15,12 C15.5522847,12 16,12.4477153 16,13 C16,");
            WriteLiteral(@"13.5522847 15.5522847,14 15,14 L13,14 L13,16 C13,16.5522847 12.5522847,17 12,17 C11.4477153,17 11,16.5522847 11,16 L11,14 Z"" fill=""#000000"" />
                                </g>
                            </svg>
                            <!--end::Svg Icon-->
                        </span>
                    </a>
                    <div class=""dropdown-menu dropdown-menu-md dropdown-menu-right p-0 m-0"">
                        <!--begin::Navigation-->
                        <ul class=""navi navi-hover"">
                            <li class=""navi-header font-weight-bold py-4"">
                                <span class=""font-size-lg"">Choose Label:</span>
                                <i class=""flaticon2-information icon-md text-muted"" data-toggle=""tooltip"" data-placement=""right"" title=""Click to learn more...""></i>
                            </li>
                            <li class=""navi-separator mb-3 opacity-70""></li>
                            <li class=""navi-item"">
           ");
            WriteLiteral(@"                     <a href=""#"" class=""navi-link"">
                                    <span class=""navi-text"">
                                        <span class=""label label-xl label-inline label-light-success"">Customer</span>
                                    </span>
                                </a>
                            </li>
                            <li class=""navi-item"">
                                <a href=""#"" class=""navi-link"">
                                    <span class=""navi-text"">
                                        <span class=""label label-xl label-inline label-light-danger"">Partner</span>
                                    </span>
                                </a>
                            </li>
                            <li class=""navi-item"">
                                <a href=""#"" class=""navi-link"">
                                    <span class=""navi-text"">
                                        <span class=""label label-xl label-inline ");
            WriteLiteral(@"label-light-warning"">Suplier</span>
                                    </span>
                                </a>
                            </li>
                            <li class=""navi-item"">
                                <a href=""#"" class=""navi-link"">
                                    <span class=""navi-text"">
                                        <span class=""label label-xl label-inline label-light-primary"">Member</span>
                                    </span>
                                </a>
                            </li>
                            <li class=""navi-item"">
                                <a href=""#"" class=""navi-link"">
                                    <span class=""navi-text"">
                                        <span class=""label label-xl label-inline label-light-dark"">Staff</span>
                                    </span>
                                </a>
                            </li>
                            <li class=""navi-sep");
            WriteLiteral(@"arator mt-3 opacity-70""></li>
                            <li class=""navi-footer py-4"">
                                <a class=""btn btn-clean font-weight-bold btn-sm"" href=""#"">
                                    <i class=""ki ki-plus icon-sm""></i>Add new
                                </a>
                            </li>
                        </ul>
                        <!--end::Navigation-->
                    </div>
                </div>
                <!--end::Dropdown-->
            </div>
            <!--end::Toolbar-->
        </div>
    </div>
    <!--end::Subheader-->
    <!--begin::Entry-->
    <div class=""d-flex flex-column-fluid"">
        <!--begin::Container-->
        <div class=""container-fluid"">
            <!--begin::Card-->
            <div class=""card card-custom"">
                <div class=""card-header flex-wrap py-5"">
                    <div class=""card-title"">

                        <h3 class=""card-label"">
                            Cadastro de T");
            WriteLiteral("ipo de Produto\r\n                        </h3>\r\n                    </div>\r\n                </div>\r\n                \r\n                <div class=\"card-body\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "815c87269d40788915aa86a3bca528ab795b165a12568", async() => {
                WriteLiteral(@"
                        <div class=""row"">
                            <div class=""col-12 col-md-8"">
                                <div class=""form-group"">
                                    <label for=""nome"">Nome <span class=""required-label"">*</span></label>
                                    <input id=""nome"" name=""nome"" type=""text"" class=""form-control"" required>
                                </div>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-12 col-md-2"">
                                <div class=""nav flex-column nav-pills nav-secondary nav-pills-no-bd nav-pills-icons"" id=""v-tab-content"" role=""tablist"" aria-orientation=""vertical"">
                                    <a class=""nav-link active"" id=""endereco-tab"" data-toggle=""pill"" href=""#v-endereco-tab"" role=""tab"" aria-controls=""v-endereco-tab"" aria-selected=""true"">
                                        <i class=""flaticon-user-1""><");
                WriteLiteral(@"/i>
                                        Dados do Tipo de Produto
                                    </a>

                                </div>
                            </div>
                            <div class=""col-12 col-sm-10"">
                                <div class=""tab-content"" id=""tab-content"">
                                    <div class=""tab-pane fade show active"" id=""v-endereco-tab"" role=""tabpanel"" aria-labelledby=""endereco-tab"">
                                        <div class=""row"">
                                            <div class=""col-12 col-sm-12"">
                                                <div class=""form-group"" id=""formCPF"">
                                                    <label for=""descricao"">Descrição <span class=""required-label"">*</span></label>
                                                    <input id=""descricao"" name=""descricao"" type=""text"" class=""form-control"" required>
                                                </div>
        ");
                WriteLiteral(@"                                    </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class=""card-action"">
                            <div class=""float-right"">
                                <button class=""btn btn-primary"" type=""button"" onclick=""gravar()"">
                                    <span class=""btn-label"">
                                        <i class=""fa fa-save""></i>
                                    </span>
                                    Gravar
                                </button>
                                <button class=""btn btn-danger"">
                                    <span class=""btn-label"">
                                        <i class=""fas fa-times""></i>
                                    </span>
                                    Cancelar
                       ");
                WriteLiteral(@"         </button>
                            </div>
                        </div>
                        <div>
                            <span class=""text-dark-50 font-weight-bolder "">(*) Campo Obrigatorio</span>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n\r\n                </div>\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("script", async() => {
                WriteLiteral("\r\n    <!-- mascara -->\r\n\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "815c87269d40788915aa86a3bca528ab795b165a17669", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n");
            }
            );
            WriteLiteral("\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
