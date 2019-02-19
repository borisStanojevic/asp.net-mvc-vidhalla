using System;
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
        //GetTopFiveVideos
        //GetVideoWithComments
        //GetVideoWithRatingt

        IEnumerable<Video> GetAllByViews(SortingDirection sortingDirection);
        IEnumerable<Video> GetAllByTitle(SortingDirection sortingDirection);
        IEnumerable<Video> GetAllByUploader(SortingDirection sortingDirection);
        IEnumerable<Video> GetAllByDateUploaded(SortingDirection sortingDirection);

    }
}
