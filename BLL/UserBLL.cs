using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class UserBLL
    {
        UserDAO userDAO = new UserDAO();

        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            dto = userDAO.GetUserWithUsernameAndPassword(model);
            return dto;
        }

        public bool AddUser(UserDTO model)
        {
            T_User user = new T_User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.ImagePath = model.ImagePath;
            user.Surname = model.Surname;
            user.isAdmin = model.isAdmin;
            user.AddDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            user.LastUpdateDate = DateTime.Now;
            int UserID = userDAO.AddUser(user);
            LogDAO.AddLog(General.ProcessType.UserAdded, General.TableName.User, UserID);
            return true;
        }

        public List<UserDTO> GetUserData()
        {
            List<UserDTO> listDTO = new List<UserDTO>();
            listDTO = userDAO.GetUserData();
            return listDTO;
        }

        public UserDTO GetUserDataWithID(int ID)
        {
            UserDTO userDTO = new UserDTO();
            userDTO = userDAO.GetUserDataWithID(ID);
            return userDTO;
        }

        public string UpdateUser(UserDTO model)
        {
            string oldImagePath = userDAO.UpdateUser(model);
            LogDAO.AddLog(General.ProcessType.UserUpdated, General.TableName.User, model.ID);
            return oldImagePath;
        }

        public string DeleteUser(int iD)
        {
            string imagePath = userDAO.DeleteUser(iD);
            LogDAO.AddLog(General.ProcessType.UserDeleted, General.TableName.User, iD);
            return imagePath;
        }
    }
}
