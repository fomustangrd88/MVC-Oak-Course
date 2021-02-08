using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class MetaBLL
    {
        MetaDAO dao = new MetaDAO();

        public bool AddMeta(MetaDTO model)
        {
            Meta meta = new Meta();
            meta.Name = model.Name;
            meta.MetaContent = model.MetaContent;
            meta.AddDate = DateTime.Now;
            meta.LastUpdateUserID = UserStatic.UserID;
            meta.LastUpdateDate = DateTime.Now;
            int MetaID = dao.AddMeta(meta);
            LogDAO.AddLog(General.ProcessType.MetaAdded, General.TableName.Meta, MetaID);
            return true;
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> listDTO = new List<MetaDTO>();
            listDTO = dao.GetMetaData();
            return listDTO;
        }

        public MetaDTO GetMetaDataWithID(int id)
        {
            MetaDTO metaDTO = new MetaDTO();
            metaDTO = dao.GetMetaDataWithID(id);
            return metaDTO;
        }

        public bool UpdateMeta(MetaDTO model)
        {
            dao.UpdateMetal(model);
            LogDAO.AddLog(General.ProcessType.MetaUpdated, General.TableName.Meta, model.ID);
            return true;
        }

        public void DeleteMeta(int iD)
        {
            dao.DeleteMeta(iD);
            LogDAO.AddLog(General.ProcessType.MetaDeleted, General.TableName.Meta, iD);
        }
    }
}
