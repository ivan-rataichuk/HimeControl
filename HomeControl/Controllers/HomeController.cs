using HomeControl.Models;
using HomeControl.Models.BaseClasses;
using HomeControl.Models.Intefaces.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeControl.Controllers
{
    public class HomeController : Controller
    {
        HomeModel homeModel = HomeModel.GetInstance();

        public ActionResult Index()
        {
            return View(homeModel);
        }

        public ActionResult Delete(int id)
        {
            BaseDevice device = homeModel.Devices.GetById(id);
            homeModel.Devices.RemoveDevice(device);
            homeModel.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AddDevice(string type = "")
        {
            homeModel.Devices.AddDevice(type);
            homeModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SelectChannel(int id, string channel = "" )
        {
            ISelectable device = homeModel.Devices.GetById(id) as ISelectable;
            device.Channel = channel;
            homeModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetVolume(int id, int volume = 0)
        {
            ISoundable device = homeModel.Devices.GetById(id) as ISoundable;
            device.SetVolume(volume);
            homeModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetTemperature(int id, int temperature = 0)
        {
            IThermalControllable device = homeModel.Devices.GetById(id) as IThermalControllable;
            device.SetTemperature(temperature);
            homeModel.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult SwitchDevice(int id)
        {
            BaseDevice device = homeModel.Devices.GetById(id);
            device.Switch();
            homeModel.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}