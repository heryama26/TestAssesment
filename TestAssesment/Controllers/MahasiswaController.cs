using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using System.Web.Mvc;
using TestAssesment.Models;

namespace TestAssesment.Controllers
{
    public class MahasiswaController : Controller
    {
        //private string Base_URL = "http://localhost:44386/";
        private TestAssesmentEntities entities = new TestAssesmentEntities();
        List<mahasiswa> result = new List<mahasiswa>();

        public IEnumerable<object> getDataMahasiswa()
        {
            var innerJoinResult = entities.mahasiswas.Join(
                    entities.dosen_wali,
                    a => a.dosen_wali_id,
                    b => b.id,
                    (a, b) => new
                    {
                        a.id,
                        a.name,
                        a.nrp,
                        a.email,
                        a.jurusan,
                        b.nama_dosen
                    })
                .Distinct().ToList();

            return innerJoinResult;
        }
        // GET: Mahasiswa
        [System.Web.Http.HttpGet]
        public JsonResult getMahasiswa()
        {
            return Json(getDataMahasiswa(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDosen()
        {
            List<dosen_wali> resultDosen = new List<dosen_wali>();
            foreach (dosen_wali mhs in entities.dosen_wali.ToList())
            {
                resultDosen.Add(mhs);
            }
            return Json(resultDosen, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpGet]
        public JsonResult getMahasiswaById(int id)
        {
            var itemById = entities.mahasiswas.Join(
                entities.dosen_wali,
                a => a.dosen_wali_id,
                b => b.id,
                (a, b) => new
                {
                    a.id,
                    a.name,
                    a.nrp,
                    a.email,
                    a.jurusan,
                    b.nama_dosen
                }).ToArray()
                .Where(x => x.id == id);

            return Json(itemById, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpPost]
        public ActionResult createMahasiswa([FromBody] mahasiswa mahasiswa)
        {
            using (var scope = new TransactionScope())
            {
                using (entities)
                {
                    try
                    {
                        entities.mahasiswas.Add(mahasiswa);
                        entities.SaveChanges();
                        int id = mahasiswa.id;
                    }
                    catch(Exception e)
                    {
                        return HttpNotFound();
                    }

                }

                scope.Complete();
                return Json("Data has been saved", JsonRequestBehavior.AllowGet);
            }
        }
        [System.Web.Http.HttpPost]
        public ActionResult deleteMahasiswa(int id)
        {
            using (var scope = new TransactionScope())
            {
                using (entities)
                {
                    var itemToRemove = entities.mahasiswas.SingleOrDefault(x => x.id == id); //returns a single item.
                    if (itemToRemove != null)
                    {
                        entities.mahasiswas.Remove(itemToRemove);
                        entities.SaveChanges();
                    }
                }
                scope.Complete();
                return Json("Delete data success", JsonRequestBehavior.AllowGet);
            }
        }
        [System.Web.Http.HttpPut]
        public ActionResult updateMahasiswa([FromBody] mahasiswa mahasiswa)
        {
            using (var scope = new TransactionScope())
            {
                using (entities)
                {
                    entities.Entry(mahasiswa).State = EntityState.Modified;
                    try
                    {
                        entities.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MahasisaExists(mahasiswa.id))
                        {
                            return HttpNotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                scope.Complete();
                return Json("Update data success", JsonRequestBehavior.AllowGet);
            }
        }
        private bool MahasisaExists(int id)
        {
            return entities.mahasiswas.Count(e => e.id == id) > 0;
        }
    }
}
