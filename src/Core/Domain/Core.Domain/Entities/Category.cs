namespace Core.Domain.Entities
{
    public static class CategoryRelations
    {
        public static string[]? GetSubCategories(MainCategory mainCategory) => mainCategory switch
        {
            MainCategory.Digitales => Enum.GetNames(typeof(SubCategoryDigital)),
            MainCategory.Kunst => Enum.GetNames(typeof(SubCategoryArt)),
            MainCategory.Fotographie => Enum.GetNames(typeof(SubCategoryFoto)),
            MainCategory.Musik => Enum.GetNames(typeof(SubCategoryMusik)),
            _ => null,
        };
    }


    public enum MainCategory
    {
        Digitales = 1,
        Kunst = 2,
        Fotographie = 3,
        Musik = 4
    }

    public enum SubCategoryDigital
    {
        Logos = 1,
        Flyer = 2,
        Illustrationen = 3,
        Karten = 4
    }

    public enum SubCategoryArt
    {
        Bilder = 1,
        Graffiti = 2,
        Skulpturen = 3,
        Dekoration = 4
    }

    public enum SubCategoryFoto
    {
        Portraits = 1,
        Locations = 2
    }

    public enum SubCategoryMusik
    {
        Events = 1,
        Online = 2
    }

}
