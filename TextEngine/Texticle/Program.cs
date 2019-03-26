using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Texticle
{
    class Program
    {
        static void Main(string[] args)
        {
            var document = XDocument.Load(@"Data\Room201.xml");

            //var scenes = document.Descendants("scene");
            var scenes = document.Element("room").Element("scenes").Elements("scene")
                .Select(e => new
                {
                    Name = e.Element("name").Value,
                    Text = e.Element("text").Value
                });

        foreach(var scene in scenes)
            {
                Console.WriteLine(scene.Name);
                Console.Write(scene.Text);
                Console.WriteLine("--------------------------------------");
            }

            Console.Read();
        }
    }
}
