using MyCalculator.Common.Helpers;
using MyCalculator.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyCalculator.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private string DbTablePath { get; } = "\\DB\\Payment.json";
        public PaymentRepository()
        {
            FileHelper.CreateFile(DbTablePath);
        }
        public Payment Create(Payment _object)
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

        public void Delete(Payment _object)
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

        public IEnumerable<Payment> GetAll()
        {
            try
            {
                var objectList =  JsonConvert.DeserializeObject<List<Payment>>(FileHelper.ReadAlltextInSelectedFile(DbTablePath));

                if(objectList == null ) 
                { 
                    return new List<Payment>();
                }

                return objectList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Payment GetById(string Id)
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

        public void Update(Payment _object)
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

        public List<Payment> GetUserPaymentList(string userId)
        {
            return GetAll().Where(x => x.UserId == userId).ToList();
        }
    }
}
