﻿using Framework.Domain;

namespace Marketer.Domain.Entities.Products
{
    public class Category : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }

        //public List<Product> Products { get; private set; }

        public Category(string name, string description, string keyWords, /*string picture, string pictureAlt, string pictureTitle,*/ string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            KeyWords = keyWords;
            //Picture = picture;
            //PictureAlt = pictureAlt;
            //PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, string description, string keyWords, /*string picture, string pictureAlt, string pictureTitle,*/ string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            KeyWords = keyWords;

            //if (!string.IsNullOrWhiteSpace(picture))
            //    Picture = picture;

            //PictureAlt = pictureAlt;
            //PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
