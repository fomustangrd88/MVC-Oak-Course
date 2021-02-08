using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class MetaDAO : PostContext
    {
        public int AddMeta(Meta meta)
        {
            try
            {
                db.Metas.Add(meta);
                db.SaveChanges();
                return meta.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> metaDTOList = new List<MetaDTO>();
            List<Meta> metaList = db.Metas.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            foreach (var item in metaList)
            {
                MetaDTO metaDTO = new MetaDTO();
                metaDTO.ID = item.ID;
                metaDTO.Name = item.Name;
                metaDTO.MetaContent = item.MetaContent;
                metaDTOList.Add(metaDTO);
            }
            return metaDTOList;
        }

        public MetaDTO GetMetaDataWithID(int id)
        {
            Meta meta = db.Metas.First(x => x.ID == id);
            MetaDTO metaDTO = new MetaDTO();
            metaDTO.ID = meta.ID;
            metaDTO.Name = meta.Name;
            metaDTO.MetaContent = meta.MetaContent;
            return metaDTO;
        }

        public void UpdateMetal(MetaDTO model)
        {
            try
            {
                Meta meta = db.Metas.First(x => x.ID == model.ID);
                meta.Name = model.Name;
                meta.MetaContent = model.MetaContent;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteMeta(int iD)
        {
            try
            {
                Meta meta = db.Metas.First(x => x.ID == iD);
                meta.isDeleted = true;
                meta.DeletedDate = DateTime.Now;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
