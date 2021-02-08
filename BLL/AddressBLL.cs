using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL;
using DTO;

namespace BLL
{
    public class AddressBLL
    {
        AddressDAO dao = new AddressDAO();
        public bool AddAddress(AddressDTO model)
        {
            Address address = new Address();
            address.Address1 = model.Address;
            address.ID = model.ID;
            address.Email = model.Email;
            address.Phone = model.Phone;
            address.Phone2 = model.Phone2;
            address.Fax = model.Fax;
            address.MapPathLarge = model.MapPathLarge;
            address.MapPathSmall = model.MapPathSmall;
            address.AddDate = DateTime.Now;
            address.AddDate = DateTime.Now;
            address.LastUpdateUserID = UserStatic.UserID;
            address.LastUpdateDate = DateTime.Now;
            int id = dao.AddAddress(address);
            LogDAO.AddLog(General.ProcessType.AddressAdded, General.TableName.Address, id);
            return true;
        }

        public List<AddressDTO> GetAddresses()
        {
            return dao.GetAddresses();
        }

        public AddressDTO GetAddressByID(int iD)
        {
            return dao.GetAddressByID(iD);
        }

        public bool UpdateAddress(AddressDTO model)
        {
            dao.UpdateAddress(model);
            LogDAO.AddLog(General.ProcessType.AddressUpdated, General.TableName.Address, model.ID);
            return true;
        }

        public void DeleteAddress(int iD)
        {
            dao.DeleteAddress(iD);
            LogDAO.AddLog(General.ProcessType.AddressDeleted, General.TableName.Address, iD);
        }
    }
}
