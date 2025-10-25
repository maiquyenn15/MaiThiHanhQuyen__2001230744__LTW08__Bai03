using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWeb08_Bai03.Models;
using System.Data.Entity;

namespace LTWeb08_Bai03.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Index
        public ActionResult Index()
        {
            var list = db.Employee.Include(e => e.tbl_Deparment).ToList();
            return View(list);
        }

        //Thêm mới nhân viên
        public ActionResult Create()
        {
            ViewBag.DeptId = new SelectList(db.Deparment, "DeptId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_Employee emp, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string path = Server.MapPath("~/Content/Images/" + emp.Id + ".jpg");
                    ImageFile.SaveAs(path);
                }

                db.Employee.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptId = new SelectList(db.Deparment, "DeptId", "Name", emp.DeptId);
            return View(emp);
        }

        //Chỉnh sửa
        public ActionResult Edit(int id)
        {
            var emp = db.Employee.Find(id);
            if (emp == null) return HttpNotFound();
            ViewBag.DeptId = new SelectList(db.Deparment, "DeptId", "Name", emp.DeptId);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(db.Deparment, "DeptId", "Name", emp.DeptId);
            return View(emp);
        }

        //Xem chi tiết
        public ActionResult Details(int id)
        {
            var emp = db.Employee.Include(e => e.tbl_Deparment)
                                 .FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return HttpNotFound();
            return View(emp);
        }

        //Xóa
        public ActionResult Delete(int id)
        {
            var emp = db.Employee.Find(id);
            if (emp == null) return HttpNotFound();
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var emp = db.Employee.Find(id);
            db.Employee.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Lấy dữ liệu từ 2 bảng Deparment & Employee
        public ActionResult DepartmentEmployee()
        {
            var data = from emp in db.Employee
                       join dept in db.Deparment on emp.DeptId equals dept.DeptId into empDept
                       from dept in empDept.DefaultIfEmpty()
                       select new DepartmentEmployeeViewModel
                       {
                           Id = emp.Id,
                           Name = emp.Name,
                           Gender = emp.Gender,
                           City = emp.City,
                           DeptId = emp.DeptId,
                           DeptName = dept != null ? dept.Name : ""
                       };
            return View(data.ToList());
        }

        //Danh sách Tên PB dạng SlideBar
        public ActionResult DepartmentList()
        {
            var departments = db.Deparment.Select(d => new DanhSachTenPB
            {
                DeptId = d.DeptId,
                Name = d.Name
            }).ToList();
            return View(departments);
        }

        public ActionResult EmployeeList(int? deptId = null)
        {
            var data = from emp in db.Employee
                       join dept in db.Deparment on emp.DeptId equals dept.DeptId into empDept
                       from dept in empDept.DefaultIfEmpty()
                       where deptId == null || emp.DeptId == deptId
                       select new DepartmentEmployeeViewModel
                       {
                           Id = emp.Id,
                           Name = emp.Name,
                           Gender = emp.Gender,
                           City = emp.City,
                           DeptId = emp.DeptId,
                           DeptName = dept != null ? dept.Name : ""
                       };
            ViewBag.Departments = db.Deparment.Select(d => new DanhSachTenPB
            {
                DeptId = d.DeptId,
                Name = d.Name
            }).ToList();
            return View(data.ToList());
        }
    }
}