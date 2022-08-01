using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace InterviewQuestions
{
    /*  
     *  The genetic code is a set of rules by which DNA or mRNA is translated into proteins (amino acid sequences).
     *  
     *  1) Three nucleotides (or tri-nucleotide), called codons, specify which amino acid will be used.
     *  2) Since codons are defined by three nucleotides, every sequence can be read in three reading frames, depending on the starting point.
     *     The actual reading frame used for translation is determined by a start codon. 
     *     In our case, we will define the start codon to be the most commonly used ATG (in some organisms there may be other start codons).
     *  3) Translation begins with the start codon, which is translated as Methionine (abbreviated as 'M').
     *  4) Translation continues until a stop codon is encountered. There are three stop codons (TAG, TGA, TAA)
     *  
     * 
     *  Included in this project is a comma seperated value (CSV) text file with the codon translations.
     *  Each line of the file has the codon, followed by a space, then the amino acid (or start or stop)
     *  For example, the first line:
     *  CTA,L
     *  should be interpreted as: "the codon CTA is translated to the amino acid L"
     *  
     *  
     *  You should not assume that the input sequence begins with the start codon. Any nucleotides before the start codon should be ignored.
     *  You should not assume that the input sequence ends with the stop codon. Any nucleotides after the stop codon should be ignored.
     * 
     *  For example, if the input DNA sequence is GAACAAATGCATTAATACAAAAA, the output amino acid sequence is MH.
     *  GAACAA ATG CAT TAA TACAAAAA
     *         \ / \ /
     *          M   H
     *          
     *  ATG -> START -> M
     *  CAT -> H
     *  TAA -> STOP
     *  
     */


    public class CodonTranslator
    {
        private Dictionary<string, string> start = new Dictionary<string, string>();
        private Dictionary<string, string> stop = new Dictionary<string, string>();
        private Dictionary<string, string> codon = new Dictionary<string, string>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codonTableFileName">Filename of the DNA codon table.</param>
        public CodonTranslator(string codonTableFileName)
        {
            var file = new StreamReader(codonTableFileName);
            var fileContent = file.ReadToEnd();
            BuildTranslationMapFromFileContent(fileContent, Path.GetExtension(codonTableFileName));
        }

        private void BuildTranslationMapFromFileContent(string fileContent, string fileType)
        {
            switch (fileType)
            {
                case ".xml":
                    throw new System.Exception("Unimplemented file type");
                case ".json":
                    JsonMapping(fileContent);
                    break;
                case ".csv":
                    CsvMapping(fileContent);
                    break;
                default:
                    throw new System.Exception("Unknown file type");
            }
        }

        private void JsonMapping(string fileContent)
        {
            Dictionary<string, object> JsonDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(fileContent);
            foreach(KeyValuePair<string, object> kv in JsonDictionary)
            {
                if(kv.Key == "Starts")
                {
                    //foreach(object value in (KeyValuePair<string, object>) kv.Value)
                    //{
                    //    start.Add(value.ToString(), "M");
                    //}
                }
                else if(kv.Key == "Stops")
                {
                    //foreach (object value in kv.Value)
                    //{
                    //    stop.Add(value.ToString(), "");
                    //}
                }
                else
                {
                    //foreach (object value in kv.Value)
                    //{
                    //    codon.Add(value[0].Value, value[1].Value);
                    //}
                }
            }
        }

        private void CsvMapping(string fileContent)
        {
            string[] codonList = fileContent.Replace("\n", "").Split('\r');
            foreach (string codonMapping in codonList)
            {
                string[] mapping = codonMapping.Split(',');
                if (mapping[1].ToUpper() == "START")
                {
                    start.Add(mapping[0], "M");
                }
                else if (mapping[1].ToUpper() == "STOP")
                {
                    stop.Add(mapping[0], "");
                }
                else
                {
                    codon.Add(mapping[0], mapping[1]);
                }
            }
        }

        /// <summary>
        /// Translates a sequence of DNA into a sequence of amino acids.
        /// </summary>
        /// <param name="dna">DNA sequence to be translated.</param>
        /// <returns>Amino acid sequence</returns>
        public string Translate(string dna)
        {
            string result = "";
            //Keeps track of location in sequence
            int index = 0;
            bool startFound = false;
            //Looks for the start of the sequence
            for (; index < dna.Length; index++)
            {
                if (start.ContainsKey(dna.Substring(index, 3).ToUpper()))
                {
                    result += start[dna.Substring(index, 3).ToUpper()];
                    index += 3;
                    startFound = true;
                    break;
                }
            }
            //Makes sure there is a start to the sequence before moving on
            if (startFound == false)
                throw new System.Exception("Codon Not Found");
            //Translates the rest of the codon
            for (; index < dna.Length - 2; index += 3)
            {
                //Stops as soon as a stop codon is located
                if (stop.ContainsKey(dna.Substring(index, 3).ToUpper()))
                    return result;
                else if (codon.ContainsKey(dna.Substring(index, 3).ToUpper()))
                    result += codon[dna.Substring(index, 3).ToUpper()];
                else
                    throw new System.Exception("Codon Not Found");
            }
            return result;
        }
    }
}