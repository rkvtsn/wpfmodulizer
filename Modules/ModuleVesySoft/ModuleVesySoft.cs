using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Drawing;
using ServerVesy;

using WpfModulizer.Library;


namespace ModuleVesySoft
{
    public class ModuleVesySoft : ModuleService<VesySoftService, ConfigModel> { }


    public class VesySoftService : IService
    {
        private Thread _thread;
        private dynamic _serverObj;
        private const string UserName = "Admin";
        private bool _stop;

        public VesySoftService()
        {
            //return;// TODO Exception
            _thread = new Thread(ServerRequest);
            _stop = false;
            Load();
            EventAggregator.Subscribe("ServerVesySetNull", (x) => _serverObj.SetNULL());
            EventAggregator.Subscribe("ServerVesySetDocument", CreateDocument);
        }

        private void CreateDocument(EventMessage eventMessage)
        {
            _serverObj.SaveEvents(3, "", UserName);
            _serverObj.SetDocuments();
            /*
             "", TextBoxNomer.Text, "", CDate(DateTimePicker1.Text),
      Format(Now, "T"), "", TextBoxOtpr.Text, TextBoxPoluch.Text, "", "", "Москва", "", "Мухосранск", "", "Вершки и корешки", "", "", _
      "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", MassaDoc, "", "", "", ""
             */
        }


        public void Stop()
        {
            _stop = true;
        }

        public void Run()
        {
            _stop = false;
        }

        public void Close()
        {
            try
            {
                //TODO
                _serverObj.SetLogout(UserName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            _thread.Abort();
        }

        private void Load()
        {
            try
            {
                _serverObj = new ServerVesy.DCOMVesy();
                _serverObj.SetLogin(UserName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            _thread.IsBackground = true;
            _thread.Start();
        }



        private void ServerRequest()
        {
            if (_stop) return;
            try
            {
                do
                {
                    PublishData();

                    PublishCam(1);
                    //PublishCam(2);
                    //PublishCam(0);

                    Thread.Sleep(50);
                } while (true);
            }
            catch (Exception e)
            {
                EventAggregator.Publish("Error", message: e.Message);
                throw new Exception(e.Message);
            }
        }
        private void PublishData()
        {
            if (!EventAggregator.IfSubscribed("VesySoft")) return;
            var data = new Dictionary<string, dynamic>();

            data["StateProtokol"] = _serverObj.StateProtokol;
            data["MASSA"] = _serverObj.MASSA;
            data["FREQ"] = _serverObj.FREQ;
            data["fSTABIL"] = _serverObj.fSTABIL;

            EventAggregator.Publish("VesySoft", data);
        }
        private void PublishCam(int camN)
        {
            var eventName = "VesySoftCam" + camN;
            if (!EventAggregator.IfSubscribed(eventName)) return;

            List<Byte> bytes = new List<byte>();
            var cam =
                (camN == 1) ? _serverObj.Cam1Jpeg :
                (camN == 2) ? _serverObj.Cam2Jpeg :
                _serverObj.Cam3Jpeg;

            if (cam == null || !(cam is IEnumerable<Byte>))
            {
                Bitmap emptyImg = new Bitmap(16, 16);
                EventAggregator.Publish(eventName, new EventMessage { Message = emptyImg });
                return;
            }
            foreach (Byte b in cam) bytes.Add(b);

            Image img = Image.FromStream(new MemoryStream(bytes.ToArray()));
            EventAggregator.Publish(eventName, new EventMessage { Message = img });
        }

    }
}
