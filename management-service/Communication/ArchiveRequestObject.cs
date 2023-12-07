namespace management_service.Communication
{
    public class ArchiveRequestObject
    {
        /// <summary>
        /// Not sure what this will do but hey, it's here
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// What method name will have to be called
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Should it always shit out the max amount of items or na
        /// </summary>
        public int Occurences { get; set; } = 0;

        /// <summary>
        /// HIGHLY dependant on what the MethodName is
        /// In case of adding this object is the ENTIRE object that is to be added
        /// In case of searching by name it is only the NAME on which to search
        /// </summary>
        public string Additionals { get; set; }

    }

    public enum Type
    {
        Article,
        Media
    }
}
