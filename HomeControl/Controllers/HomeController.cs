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
            return RedirectToAction("Index");
        }

        public ActionResult AddDevice(string type = "")
        {
            homeModel.Devices.AddDevice(type);
            return RedirectToAction("Index");
        }

        public ActionResult SaveChanges()
        {
            homeModel.SaveInstance();
            return RedirectToAction("Index");
        }

        public ActionResult SelectChannel(int id, string channel = "" )
        {
            ISelectable device = (ISelectable)homeModel.Devices.GetById(id);
            device.Channel = channel;
            return RedirectToAction("Index");
        }

        public ActionResult SetVolume(int id, int volume = 0)
        {
            ISoundable device = (ISoundable)homeModel.Devices.GetById(id);
            device.SetVolume(volume);
            return RedirectToAction("Index");
        }

        public ActionResult SetTemperature(int id, int temperature = 0)
        {
            IThermalControllable device = (IThermalControllable)homeModel.Devices.GetById(id);
            device.SetTemperature(temperature);
            return RedirectToAction("Index");
        }

        public ActionResult SwitchDevice(int id, bool isSwitched = false)
        {
            BaseDevice device = homeModel.Devices.GetById(id);
            if (isSwitched)
            {
                device.SwitchOn();
            }
            else
            {
                device.SwitchOff();
            }
            return RedirectToAction("Index");
        }
    }
}