using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrueFalseInterface
{
    class TrueFalse
    {
        #region Private fields
        string fileName;
        private List<Question> list;
        #endregion

        #region Public properties

        public int Count()
        {
            return list.Count;
        }
        public string FileName
        {
            set { fileName = value; }
        }
        #endregion

        #region Public Methods
        public TrueFalse(string fileName)
        {
            this.fileName = fileName;
            list = new List<Question>();
        }
        public Question this[int index]
        {
            get
            {
                if (index >= list.Count || index < 0)
                {
                    throw new Exception("Такого вопроса не существует!");
                }
                else return list[index];
            }
        }
        public void Add(string text, bool trueFalse)
        {
            list.Add(new Question(text, trueFalse));
        }
        public void Add(Question question)
        {
            list.Add(question);
        }
        public void Remove(int index)
        {
            if (list != null && index < list.Count && index >= 0)
                list.RemoveAt(index);
        }
        public void Load()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            list = (List<Question>)xmlSerializer.Deserialize(stream);
            stream.Close();
        }
        public void Save()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(stream, list);
            stream.Close();
        }
        #endregion
    }
}
