using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class VideoBLL
    {
        VideoDAO dao = new VideoDAO();

        public bool AddVideo(VideoDTO model)
        {
            Video video = new Video();
            video.Title = model.Title;
            video.VideoPath = model.VideoPath;
            video.OrigialVideoPath = model.OriginalVideoPath;
            video.AddUserID = UserStatic.UserID;
            video.AddDate = DateTime.Now;
            video.LastUpdateDate = DateTime.Now;
            video.LastUpdateUserID = UserStatic.UserID;
            int id = dao.AddVideo(video);
            LogDAO.AddLog(General.ProcessType.VideoAdded, General.TableName.Video, id);

            return true;
        }

        public List<VideoDTO> GetVideoList()
        {
            return dao.GetVideoList();
        }

        public VideoDTO GetVideoById(int id)
        {
            return dao.GetVideoById(id);
        }

        public bool UpdateVideo(VideoDTO model)
        {
            dao.UpdateVideo(model);
            LogDAO.AddLog(General.ProcessType.VideoUpdated, General.TableName.Video, model.ID);
            return true;
        }

        public void DeleteVideo(int iD)
        {
            dao.DeleteVideo(iD);
            LogDAO.AddLog(General.ProcessType.VideoDeleted, General.TableName.Video, iD);
        }
    }
}
