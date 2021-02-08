using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class UserDAO : PostContext
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            T_User user = db.T_User.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if(user != null && user.ID != 0)
            {
                dto.ID = user.ID;
                dto.Username = user.Username;
                dto.Email = user.Email;
                dto.Surname = user.Surname;
                dto.ImagePath = user.ImagePath;
                dto.isAdmin = user.isAdmin;
            }

            return dto;
        }

        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserDTO> GetUserData()
        {
            List<UserDTO> userListDTO = new List<UserDTO>();
            List<T_User> userList = db.T_User.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();

            foreach (var item in userList)
            {
                UserDTO dto = new UserDTO();
                dto.ID = item.ID;
                dto.Username = item.Username;
                dto.Password = item.Password;
                dto.Email = item.Email;
                dto.Surname = item.Surname;
                dto.ImagePath = item.ImagePath;
                dto.isAdmin = item.isAdmin;
                userListDTO.Add(dto);
            }

            return userListDTO;
        }

        public string UpdateUser(UserDTO model)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == model.ID);
                string oldImagePath = user.ImagePath;
                user.Username = model.Username;
                user.Password = model.Password;
                user.Email = model.Email;
                user.Surname = model.Surname;
                if (model.ImagePath != null)
                {
                    user.ImagePath = model.ImagePath;
                }
                user.isAdmin = model.isAdmin;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

                return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteUser(int iD)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == iD);
                user.isDeleted = true;
                user.DeletedDate = DateTime.Now;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

                return user.ImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserDTO GetUserDataWithID(int ID)
        {
            T_User user = db.T_User.First(x => x.ID == ID);
            UserDTO userDTO = new UserDTO();
            userDTO.ID = user.ID;
            userDTO.Username = user.Username;
            userDTO.Password = user.Password;
            userDTO.Email = user.Email;
            userDTO.Surname = user.Surname;
            userDTO.ImagePath = user.ImagePath;
            userDTO.isAdmin = user.isAdmin;

            return userDTO;
        }
    }
}
