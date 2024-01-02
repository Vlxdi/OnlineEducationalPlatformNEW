using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineEducationalPlatformNEW.Data;
using OnlineEducationalPlatformNEW.Models;

namespace OnlineEducationalPlatformNEW.Controllers
{
    [Authorize(Roles = "Teacher, Student, Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Teacher"))
            {
                var courses = await _context.Courses.ToListAsync();
                return View(courses);
            }
            else if (User.IsInRole("Student"))
            {
                var courses = await _context.Courses.Where(c => c.IsPublic).ToListAsync();
                return View(courses);
            }

            // Default behavior for other roles
            return View();
        }


        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Debugging: Output user roles and course details to console
            var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            Console.WriteLine($"User Roles: {string.Join(", ", userRoles)}");
            Console.WriteLine($"Course Id: {course.Id}, IsPublic: {course.IsPublic}");

            // Simplified check: If the user is a student, grant access
            if (User.IsInRole("Student") || User.IsInRole("Teacher"))
            {
                return View(course);
            }

            // For all other cases, deny access
            return Forbid();
        }


        // GET: Courses/EnterCourseId
        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult EnterCourseId()
        {
            return View();
        }

        // POST: Courses/EnterCourseId
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EnterCourseId(string courseId)
        {
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);

                if (course != null && (course.IsPublic.Equals(false) || User.IsInRole("Teacher")))
                {
                    return RedirectToAction(nameof(Details), new { id = course.Id });
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid course ID or you do not have permission to access this course.");
            return View();
        }


        [Authorize(Roles = "Teacher")]
        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,Name,Description,IsPublic")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                // Check ModelState Errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Console.WriteLine("ModelState Errors:");
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                _context.Add(courses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }


        [Authorize(Roles = "Teacher")]
        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,Name,Description,IsPublic")] Courses courses)
        {
            if (id != courses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesExists(courses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }


        [Authorize(Roles = "Teacher")]
        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courses = await _context.Courses.FindAsync(id);
            if (courses != null)
            {
                _context.Courses.Remove(courses);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}