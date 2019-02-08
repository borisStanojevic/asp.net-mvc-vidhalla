using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidhalla.Core.Domain;

namespace Vidhalla.Core.Repositories
{
    public interface IVideoRepository : IGenericRepository<Video>
    {
        //GetTopFiveVideos
        //GetVideoWithComments
        //GetVideoWithRating
    }
}
