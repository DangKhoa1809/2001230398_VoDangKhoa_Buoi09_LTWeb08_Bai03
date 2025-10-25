using LTWeb08_Bai03.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTWeb08_Bai03.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        QL_NhanSuEntities data = new QL_NhanSuEntities();
        public ActionResult DS_Employee()
        {
            List<Employee> ds = data.Employees.OrderBy(e => e.Id).ToList();
            return View(ds);
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.DeptId = new SelectList(data.Deparments.OrderBy(d => d.Name), "DeptId", "Name");

            var emp = new Employee
            {
                Gender = "Nam"
            };

            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoi(Employee emp, HttpPostedFileBase ImageFile)
        {
            if (string.IsNullOrWhiteSpace(emp.Name))
                ModelState.AddModelError("Name", "Vui lòng nhập tên nhân viên.");
            if (string.IsNullOrWhiteSpace(emp.City))
                ModelState.AddModelError("City", "Vui lòng nhập thành phố.");
            if (emp.DeptId == 0)
                ModelState.AddModelError("DeptId", "Vui lòng chọn phòng ban.");
            if (string.IsNullOrWhiteSpace(emp.Gender))
                ModelState.AddModelError("Gender", "Vui lòng chọn giới tính.");
            if (ImageFile == null || ImageFile.ContentLength == 0)
                ModelState.AddModelError("Image", "Vui lòng chọn ảnh.");

            if (ModelState.IsValid)
            {
                try
                {
                    string folder = Server.MapPath("~/Images");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string path = Path.Combine(folder, fileName);
                    ImageFile.SaveAs(path);
                    emp.Image = fileName;

                    data.Employees.Add(emp);
                    data.SaveChanges();

                    return RedirectToAction("DS_Employee");
                }
                catch (Exception ex)
                {
                    string err = ex.InnerException?.InnerException?.Message ?? ex.Message;
                    ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + err);
                }
            }

            ViewBag.DeptId = new SelectList(data.Deparments.OrderBy(d => d.Name), "DeptId", "Name", emp.DeptId);
            return View(emp);
        }

        [HttpGet]
        public ActionResult Sua(int id)
        {
            var emp = data.Employees.Find(id);
            if (emp == null)
                return HttpNotFound();

            ViewBag.DeptId = new SelectList(data.Deparments.OrderBy(d => d.Name), "DeptId", "Name", emp.DeptId);

            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(Employee emp, HttpPostedFileBase ImageFile)
        {
            if (string.IsNullOrWhiteSpace(emp.Name))
                ModelState.AddModelError("Name", "Vui lòng nhập tên nhân viên.");
            if (string.IsNullOrWhiteSpace(emp.City))
                ModelState.AddModelError("City", "Vui lòng nhập thành phố.");
            if (emp.DeptId == 0)
                ModelState.AddModelError("DeptId", "Vui lòng chọn phòng ban.");
            if (string.IsNullOrWhiteSpace(emp.Gender))
                ModelState.AddModelError("Gender", "Vui lòng chọn giới tính.");

            var existingEmp = data.Employees.Find(emp.Id);
            if (existingEmp == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    existingEmp.Name = emp.Name;
                    existingEmp.Gender = emp.Gender;
                    existingEmp.City = emp.City;
                    existingEmp.DeptId = emp.DeptId;

                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string folder = Server.MapPath("~/Images");
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);

                        string fileName = Path.GetFileName(ImageFile.FileName);
                        string path = Path.Combine(folder, fileName);
                        ImageFile.SaveAs(path);
                        existingEmp.Image = fileName;
                    }

                    data.SaveChanges();
                    return RedirectToAction("DS_Employee");
                }
                catch (Exception ex)
                {
                    string err = ex.InnerException?.InnerException?.Message ?? ex.Message;
                    ModelState.AddModelError("", "Lỗi khi cập nhật dữ liệu: " + err);
                }
            }

            emp.Image = existingEmp.Image;

            ViewBag.DeptId = new SelectList(data.Deparments.OrderBy(d => d.Name), "DeptId", "Name", emp.DeptId);
            return View(emp);
        }

        public ActionResult ChiTiet(int id)
        {
            var emp = data.Employees.Find(id);
            if (emp == null)
                return HttpNotFound();

            return View(emp);
        }

        public ActionResult DS_Employee_Deparment()
        {
            var ds = data.Employees.Include("Deparment").OrderBy(e => e.Id).ToList();

            return View(ds);
        }

        public ActionResult Xoa(int id)
        {
            var emp = data.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaConfirmed(int id)
        {
            var emp = data.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return HttpNotFound();

            data.Employees.Remove(emp);
            data.SaveChanges();

            TempData["SuccessMessage"] = "Xóa nhân viên thành công!";
            return RedirectToAction("DS_Employee");
        }
    }
}