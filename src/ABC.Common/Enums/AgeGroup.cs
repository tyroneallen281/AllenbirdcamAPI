using ABC.Common.Extentions;

namespace ABC.Common.Enums
{

    public enum AgeGroup
    {
        [AgeRange(0,100)]
        All,

        [AgeRange(10, 20)]
        Teens,

        [AgeRange(20, 30)]
        Twenties,

        [AgeRange(30, 40)]
        Thirties,

        [AgeRange(40, 50)]
        Forties,

        [AgeRange(50, 100)]
        FiftyAbove
    }
}
