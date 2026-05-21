Online Learning Platform 🎓

A full-stack Learning Management System (LMS) built with ASP.NET Core Web API and React.js.
The platform supports Students and Teachers with authentication, course management, assignments, submissions, grading system, dashboards, enrollments, and progress tracking.

🚀 Features
🔐 Authentication & Authorization
JWT Authentication
Role-based Authorization
Student & Teacher roles
Secure password hashing using BCrypt
Protected API endpoints
👨‍🏫 Teacher Features
Create Courses
Update/Delete Courses
Create Lessons
Create Assignments
View Student Enrollments
View Assignment Submissions
Grade Student Submissions
Teacher Dashboard Analytics
👨‍🎓 Student Features
Browse Courses
Enroll in Courses
View Lessons
Submit Assignments
Track Progress
Student Dashboard
View Grades & Feedback
🛠️ Tech Stack
Backend
ASP.NET Core Web API
Entity Framework Core
SQL Server
JWT Authentication
Swagger/OpenAPI
Frontend
React.js
React Router DOM
Axios
Context API / Local Storage Auth
Modern Responsive UI
📂 Project Structure
Backend Structure
OnlineLearningPlatform/
│
├── Controllers/
│   ├── AuthController.cs
│   ├── CoursesController.cs
│   ├── LessonsController.cs
│   ├── AssignmentsController.cs
│   ├── EnrollmentsController.cs
│   ├── SubmissionsController.cs
│   ├── StudentController.cs
│   └── TeacherController.cs
│
├── DTOs/
│   ├── CreateCourseDto.cs
│   ├── CreateAssignmentDto.cs
│   ├── SubmitAssignmentDto.cs
│   ├── GradeSubmissionDto.cs
│   └── ...
│
├── Models/
│   ├── User.cs
│   ├── Course.cs
│   ├── Lesson.cs
│   ├── Assignment.cs
│   ├── Submission.cs
│   └── Enrollment.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Services/
│   └── AuthService.cs
│
├── Program.cs
├── appsettings.json
└── ...
Frontend Structure
src/
│
├── components/
│   ├── Layout.jsx
│   ├── Navbar.jsx
│   ├── ProtectedRoute.jsx
│   └── ...
│
├── pages/
│   ├── Login.jsx
│   ├── Register.jsx
│   ├── Courses.jsx
│   ├── CourseDetails.jsx
│   ├── StudentDashboard.jsx
│   ├── TeacherDashboard.jsx
│   ├── Assignments.jsx
│   └── ...
│
├── services/
│   └── api.js
│
├── App.jsx
├── main.jsx
└── index.css
⚙️ Backend Setup
1️⃣ Clone Repository
git clone https://github.com/MRXilo/OnlineLearningPlatform.git
cd OnlineLearningPlatform
2️⃣ Configure Database

Open:

appsettings.json

Update connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=OnlineLearningDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
3️⃣ Run Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
4️⃣ Run Backend
dotnet run

Backend runs on:

https://localhost:7028

Swagger:

https://localhost:7028/swagger
⚛️ Frontend Setup
1️⃣ Navigate to Frontend
cd frontend
2️⃣ Install Dependencies
npm install
3️⃣ Start React App
npm run dev

Frontend runs on:

http://localhost:5173
🔑 Authentication

This project uses JWT Tokens.

After login/register:

localStorage.setItem("token", token);

Protected routes use:

Authorization: Bearer YOUR_TOKEN
📌 Main API Endpoints
Auth
Method	Endpoint	Description
POST	/api/Auth/register	Register user
POST	/api/Auth/login	Login
Courses
Method	Endpoint	Description
GET	/api/Courses	Get all courses
GET	/api/Courses/{id}	Get course by id
POST	/api/Courses	Create course
PUT	/api/Courses/{id}	Update course
DELETE	/api/Courses/{id}	Delete course
Enrollments
Method	Endpoint	Description
POST	/api/Enrollments/{courseId}	Enroll student
GET	/api/Enrollments/my	My courses
GET	/api/Enrollments/student/dashboard	Student dashboard
Assignments
Method	Endpoint	Description
POST	/api/Assignments	Create assignment
GET	/api/Assignments/course/{courseId}	Course assignments
Submissions
Method	Endpoint	Description
POST	/api/Submissions/{assignmentId}/submit	Submit assignment
GET	/api/Submissions/assignment/{assignmentId}	Get submissions
PUT	/api/Submissions/{id}/grade	Grade submission
GET	/api/Submissions/progress	Student progress
🧠 Database Design
Main Entities
Users
Courses
Lessons
Assignments
Submissions
Enrollments
Relationships
One Teacher → Many Courses
One Course → Many Lessons
One Course → Many Assignments
One Assignment → Many Submissions
Many Students ↔ Many Courses (Enrollments)
🔒 Security Features
JWT Token Authentication
Role-based Access
Password Hashing (BCrypt)
Protected Controllers
Secure API Endpoints
📷 Screenshots
Suggested Screenshots to Add
Login Page
Register Page
Courses Page
Student Dashboard
Teacher Dashboard
Assignment Submission Page
Swagger API
🌟 Future Improvements
File Uploads
Video Lessons
Email Notifications
Real-time Chat
Admin Panel
Course Certificates
Payment Integration
Dark Mode
Search & Filtering
🧪 Testing

You can test APIs using:

Swagger UI
Postman
Thunder Client
👨‍💻 Author

Developed by mrmrc

GitHub Repository:

https://github.com/MRXilo/OnlineLearningPlatform
📄 License

This project is licensed for educational purposes.

⭐ Support

If you like this project:

Star the repository ⭐
Fork the project 🍴
Share with others 🚀
💡 Notes

This project was developed as a Diploma / Graduation Project using:

ASP.NET Core
Entity Framework Core
SQL Server
React.js

The goal was to build a complete modern LMS platform with real-world backend and frontend architecture.
