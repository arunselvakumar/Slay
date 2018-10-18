﻿namespace Slay.Models.BusinessObjects.Category
{
    public sealed class CreateCategoryRequestBo
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsEnabled { get; set; }
    }
}