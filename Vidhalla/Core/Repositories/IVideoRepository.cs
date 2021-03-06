﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vidhalla.Core.Domain;

namespace Vidhalla.Core.Repositories
{
    public interface IVideoRepository : IGenericRepository<Video>
    {

        IEnumerable<Video> GetAllByViews(SortingDirection sortingDirection, string searchString);
        IEnumerable<Video> GetAllByTitle(SortingDirection sortingDirection, string searchString);
        IEnumerable<Video> GetAllByUploader(SortingDirection sortingDirection, string searchString);
        IEnumerable<Video> GetAllByDateUploaded(SortingDirection sortingDirection, string searchString);
        Video GetIncludeRelated(int id);

    }
}
