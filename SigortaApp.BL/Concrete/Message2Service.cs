using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class Message2Service : IMessage2Service
    {
        IMessage2Repository _message2dal;

        public Message2Service(IMessage2Repository message2dal)
        {
            _message2dal = message2dal;
        }

        public List<Message2> GetInboxListByWriter(int id)
        {
            return _message2dal.GetInBoxWithMessageByWriter(id);
        }

        public List<Message2> GetList()
        {
            return _message2dal.GetListAll();
        }

        public List<Message2> GetSendboxListByWriter(int id)
        {
            return _message2dal.GetSendBoxWithMessageByWriter(id);
        }

        public void TAdd(Message2 t)
        {
            _message2dal.Insert(t);
        }

        public void TDelete(Message2 t)
        {
            throw new NotImplementedException();
        }

        public Message2 TGetById(int id)
        {
            return _message2dal.GetById(id);
        }

        public void TUpdate(Message2 t)
        {
            throw new NotImplementedException();
        }
    }
}
