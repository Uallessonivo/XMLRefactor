using System.Xml;

namespace XMLRefactor
{
    class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<string, string> codeBase = new Dictionary<string, string>();

            using (StreamReader reader =
                   new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "base.csv")))
            {
                while (!reader.EndOfStream)
                {
                    string[] lines = reader.ReadLine().Split(";");
                    if (lines.Length >= 2)
                    {
                        codeBase.Add(lines[0], lines[1]);
                    }
                }
            }

            Console.Write("Digite o nome do arquivo XML a ser modificado: ");
            var fileName = Console.ReadLine();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", $"{fileName}.xml"));

            var nodeList = xmlDocument.GetElementsByTagName("cProd");

            for (int i = 0; i < nodeList.Count; i++)
            {
                if (codeBase.ContainsKey(nodeList[i].InnerText))
                {
                    nodeList[i].InnerText = codeBase[nodeList[i].InnerText];
                }
                else
                {
                    Console.WriteLine($"O código: {nodeList[i].InnerText} não foi encontrado no arquivo Base.");
                    Console.WriteLine($"Atualize o arquivo e execute novamente o programa...");
                }
            }

            var numNfe = xmlDocument.GetElementsByTagName("nNF");
            xmlDocument.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files",
                $"{numNfe[0].InnerText}.xml"));
        }
    }
}