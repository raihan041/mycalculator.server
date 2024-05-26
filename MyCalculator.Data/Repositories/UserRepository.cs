using MyCalculator.Common.Helpers;
using MyCalculator.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string DbTablePath { get; } = "\\DB\\User.json";
        public UserRepository() 
        {
            FileHelper.CreateFile(DbTablePath);
        }
        public User Create(User _object)
        {
            try
            {
                _object.Id = Guid.NewGuid().ToString();
                _object.CreatedDate = DateTime.Now;
                FileHelper.WriteAlltextInSelectedFile(JsonConvert.SerializeObject(GetAll().Append(_object)), DbTablePath);
                return _object;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Delete(User _object)
        {
            try
            {
                var objectList = GetAll();
                var removableItem = objectList.FirstOrDefault(x => x.Id == _object.Id);
                if (removableItem != null)
                {
                    objectList.ToList().Remove(removableItem);
                }
                FileHelper.WriteAlltextInSelectedFile(JsonConvert.SerializeObject(objectList), DbTablePath);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                var objectList = JsonConvert.DeserializeObject<List<User>>(FileHelper.ReadAlltextInSelectedFile(DbTablePath));

                if (objectList == null)
                {
                    return new List<User>();
                }

                return objectList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User GetById(string Id)
        {
            try
            {
                return GetAll().FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(User _object)
        {
            try
            {
                var objectList = GetAll().ToList();
                var changableItem = objectList.FirstOrDefault(x => x.Id == _object.Id);

                int index = 0;
                foreach (var item in objectList)
                {
                    if (item.Id == _object.Id)
                    {
                        _object.UpdatedDate = DateTime.Now;
                        objectList[index] = _object;
                        break;
                    }
                    index++;
                }

                FileHelper.WriteAlltextInSelectedFile(JsonConvert.SerializeObject(objectList), DbTablePath);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public User GetByEmail(string email)
        {
            return GetAll().FirstOrDefault(x => x.Email == email);
        }
    }
}
