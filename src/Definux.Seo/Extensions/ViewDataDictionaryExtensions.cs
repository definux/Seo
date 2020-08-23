using Definux.Seo.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace Definux.Seo.Extensions
{
    public static class ViewDataDictionaryExtensions
    {
        private const string ViewDataMetaTagsModelKey = "MetaTagsModel";

        public static MetaTagsModel GetMetaTagsModelOrDefault(this ViewDataDictionary viewData)
        {
            try
            {
                return viewData.ContainsKey(ViewDataMetaTagsModelKey) ? (MetaTagsModel)viewData[ViewDataMetaTagsModelKey] : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static MetaTagsModel GetOrCreateCurrentMetaTagsModel(this ViewDataDictionary viewData)
        {
            MetaTagsModel result = new MetaTagsModel();

            try
            {
                if (viewData.ContainsKey(ViewDataMetaTagsModelKey) && viewData[ViewDataMetaTagsModelKey] != null)
                {
                    result = (MetaTagsModel)viewData[ViewDataMetaTagsModelKey];
                }
            }
            catch (Exception ex)
            {
                result = new MetaTagsModel();
            }
            finally
            {
                viewData[ViewDataMetaTagsModelKey] = result;
            }

            return result;
        }

        public static void ApplyMetaTagsModel(this ViewDataDictionary viewData, MetaTagsModel model)
        {
            viewData[ViewDataMetaTagsModelKey] = model;
        }
    }
}
