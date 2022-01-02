using System;

namespace Framework.Peresentation
{
    public static class ColorGenerator
    {
        public static string ColorProducer()
        {
            string[] colors = { "blueviolet", "brown", "cadetblue", "coral", "cornflowerblue","red", "deeppink" , "goldenrod" , "indianred" , "Indigo" , "mediumpurple" , "royalblue",
                                "skyblue"};

            Random random = new Random();

            int index = random.Next(0, 13);

            return colors[index];
        }
    }
}