using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class AddressDAO : PostContext
    {
        public int AddAddress(Address address)
        {
            try
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return address.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AddressDTO> GetAddresses()
        {
            List<AddressDTO> addListDTO = new List<AddressDTO>();
            List<Address> addList = db.Addresses.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();

            foreach (var item in addList)
            {
                AddressDTO dto = new AddressDTO();
                dto.ID = item.ID;
                dto.Address = item.Address1;
                dto.Email = item.Email;
                dto.Phone = item.Phone;
                dto.Phone2 = item.Phone2;
                dto.Fax = item.Fax;
                dto.MapPathLarge = item.MapPathLarge;
                dto.MapPathSmall = item.MapPathSmall;
                addListDTO.Add(dto);
            }

            return addListDTO;
        }

        public AddressDTO GetAddressByID(int iD)
        {
            AddressDTO dto = new AddressDTO();
            Address add = db.Addresses.First(x => x.ID == iD);
            dto.ID = add.ID;
            dto.Address = add.Address1;
            dto.Email = add.Email;
            dto.Phone = add.Phone;
            dto.Phone2 = add.Phone2;
            dto.Fax = add.Fax;
            dto.MapPathLarge = add.MapPathLarge;
            dto.MapPathSmall = add.MapPathSmall;

            return dto;
        }

        public void DeleteAddress(int iD)
        {
            try
            {
                Address add = db.Addresses.First(x => x.ID == x.ID);
                add.isDeleted = true;
                add.DeletedDate = DateTime.Now;
                add.LastUpdateDate = DateTime.Now;
                add.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateAddress(AddressDTO model)
        {
            try
            {
                Address add = db.Addresses.First(x => x.ID == model.ID);
                add.Address1 = model.Address;
                add.Email = model.Email;
                add.Phone = model.Phone;
                add.Phone2 = model.Phone2;
                add.Fax = model.Fax;
                add.MapPathLarge = model.MapPathLarge;
                add.MapPathSmall = model.MapPathSmall;
                add.LastUpdateDate = DateTime.Now;
                add.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
