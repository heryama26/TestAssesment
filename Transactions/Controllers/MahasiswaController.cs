using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transactions.Models;

namespace Transactions.Controllers
{
    public class MahasiswaController : Controller
    {
        private Uri Base_URL = new Uri("https://localhost:44386/mahasiswa");
        private HttpClient WebReq = new HttpClient();
        private HttpResponseMessage WebResp;
        List<MahasiswaModel> mahasiswaList = new List<MahasiswaModel>();
        List<DosenModel> dosenList = new List<DosenModel>();

        // GET: Mahasiswa
        public ActionResult Index()
        {
            try
            {
                WebResp = WebReq.GetAsync(Base_URL + "/getMahasiswa").Result;
                if (WebResp.IsSuccessStatusCode)
                {
                    string jsonString = WebResp.Content.ReadAsStringAsync().Result;
                    mahasiswaList = JsonConvert.DeserializeObject<List<MahasiswaModel>>(jsonString);
                }
            }
            catch(Exception e)
            {
                
            }
            return View(mahasiswaList);
        }
        public List<DosenModel> getDosen()
        {
            try
            {
                WebResp = WebReq.GetAsync(Base_URL + "/getDosen").Result;
                if (WebResp.IsSuccessStatusCode)
                {
                    string jsonString = WebResp.Content.ReadAsStringAsync().Result;
                    dosenList = JsonConvert.DeserializeObject<List<DosenModel>>(jsonString);
                }
            }
            catch (Exception e)
            {

            }
            return dosenList;
        }
        // GET: Mahasiswa/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                WebResp = WebReq.GetAsync(Base_URL + "/getMahasiswaById/" + id).Result;
                if (WebResp.IsSuccessStatusCode)
                {
                    string jsonString = WebResp.Content.ReadAsStringAsync().Result;
                    mahasiswaList = JsonConvert.DeserializeObject<List<MahasiswaModel>>(jsonString);
                }
            }
            catch (Exception e)
            {

            }
            return View(mahasiswaList[0]);
        }

        // GET: Mahasiswa/Create
        public ActionResult Create()
        {
            var items = getDosen().ToList();
            if (items != null)
            {
                ViewBag.data = items;
            }
            return View();
        }

        // POST: Mahasiswa/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                var json = JsonConvert.SerializeObject(collection);
                var location_content = new StringContent(json, Encoding.UTF8, "application/json");
                var responses = await WebReq.PostAsync(Base_URL + "/createMahasiswa/",
                                          new FormUrlEncodedContent(
                                              collection.
                                                  AllKeys.ToDictionary(
                                                      k => k, v => collection[v])));
                if (responses.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); 
                }
                else
                { 
                    return View(); 
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Mahasiswa/Edit/5
        public ActionResult Edit(int id)
        {
            var items = getDosen().ToList();
            if (items != null)
            {
                ViewBag.data = items;
            }
            return Details(id);
        }

        // POST: Mahasiswa/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var json = JsonConvert.SerializeObject(collection);
                var location_content = new StringContent(json, Encoding.UTF8, "application/json");
                var responses = await WebReq.PostAsync(Base_URL + "/updateMahasiswa/" + id,
                                          new FormUrlEncodedContent(
                                              collection.
                                                  AllKeys.ToDictionary(
                                                      k => k, v => collection[v])));
                if (responses.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Mahasiswa/Delete/5
        public ActionResult Delete(int id)
        {
            return Details(id);
        }

        // POST: Mahasiswa/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var json = JsonConvert.SerializeObject(collection);
                var location_content = new StringContent(json, Encoding.UTF8, "application/json");
                var responses = await WebReq.PostAsync(Base_URL + "/deleteMahasiswa/" + id,
                                          new FormUrlEncodedContent(
                                              collection.
                                                  AllKeys.ToDictionary(
                                                      k => k, v => collection[v])));
                if (responses.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
