using Domain.Common;

namespace Domain.Entities
{
    public class Media: Entity
    {   
        public string Name { get; set; }
        public string Link {  get; set; }

        public Media(string name, string link)
        {
            Name = name;
            Link = link;
        }
    }
}
