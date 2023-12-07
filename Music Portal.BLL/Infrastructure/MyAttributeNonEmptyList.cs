using System.ComponentModel.DataAnnotations;

namespace Music_Portal.BLL.Infrastructure
{
    // Перевіряє колекцію List, що вона не порожня
    public class MyAttributeNonEmptyList : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {   
                List<string> listToCheck = value as List<string>;
                if (listToCheck.Count > 0) { return true; }
            }
            
            return false;
        }
    }
}
