using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        #region Initialize
        private IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }
        #endregion

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbproductCategory = _productCategoryService.GetById(productCategoryViewModel.ID);
                    dbproductCategory.UpdateProductCategory(productCategoryViewModel);
                    dbproductCategory.UpdatedDate = DateTime.Now;
                    _productCategoryService.Update(dbproductCategory);
                    _productCategoryService.SaveChanges();
                    var responData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbproductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responData);
                }
                return response;
            });
        }

        [Route("getbyid{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetById(id);
                var respondData = Mapper.Map<ProductCategoryViewModel>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, respondData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listCategory = _productCategoryService.GetAll(keyword);
                totalRow = listCategory.Count();
                var query = listCategory.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var listPostCategoryVm = Mapper.Map<List<ProductCategoryViewModel>>(query);
                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listPostCategoryVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow/pageSize)
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listCategory = _productCategoryService.GetAll();
                var listPostCategoryVm = Mapper.Map<List<ProductCategoryViewModel>>(listCategory);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var productCategory = new ProductCategory();
                    productCategory.UpdateProductCategory(productCategoryViewModel);
                    productCategory.CreatedDate = DateTime.Now;
                    _productCategoryService.Add(productCategory);
                    _productCategoryService.SaveChanges();
                    var responData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var productCategory = _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();
                    var responData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, responData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listId)
        {
            //Chuyen listId JSON sang list cac Id
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var Ids = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in Ids)
                    {
                        _productCategoryService.Delete(id);
                    }

                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, Ids.Count);
                }

                return response;
            });
        }
    }
}