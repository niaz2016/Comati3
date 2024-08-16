using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Comati3.DTOs
{
    public class BaseDTO
    {
        public T ToModel<T>() where T : new()
        {
            T obj = new T();

            PropertyInfo[] dtoProperties = this.GetType().GetProperties();
            PropertyInfo[] modelProperties = typeof(T).GetProperties();

            foreach (var dtoProp in dtoProperties)
            {
                PropertyInfo correspondingProp = modelProperties.FirstOrDefault(p => p.Name == dtoProp.Name);
                if (correspondingProp != null && correspondingProp.CanWrite)
                {
                    if (correspondingProp.CanWrite)
                    {
                        correspondingProp.SetValue(obj, dtoProp.GetValue(this));
                    }
                }
            }

            return obj;
        }

        public bool IsValid
        {
            get
            {
                var context = new ValidationContext(this, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                return Validator.TryValidateObject(this, context, results, validateAllProperties: true);
            }

        }
    }
}
