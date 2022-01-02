using Framework.Domain;
using System;
using System.Collections.Generic;

namespace Marketer.Domain.Entities.Products
{
    public class Brand : EntityBase
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }

        public List<Product> Products { get; private set; }

        public Brand(string name, string picture, string pictureAlt, string pictureTitle, string keyWords, string metaDescription, string slug)
        {
            Name = name;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, string picture, string pictureAlt, string pictureTitle, string keyWords, string metaDescription, string slug)
        {
            Name = name;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;

            LastUpdateDate = DateTime.Now;
        }
    }
}