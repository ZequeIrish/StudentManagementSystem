// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.






// Add Course Modal
function openAddCourseModal() {
    document.getElementById("addCourseModal").style.display = "block";
}

function closeAddCourseModal() {
    document.getElementById("addCourseModal").style.display = "none";
}


// Add for save button
async function addCourse() {

    const courseName =
        document.getElementById("courseName").value.trim();

    if (courseName === "") {
        alert("Please enter a course name.");
        return;
    }

    try {

        const response = await fetch("/Courses/Create", {
            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                courseName: courseName
            })
        });

        if (!response.ok) {
            alert("Failed to add course.");
            return;
        }

        closeAddCourseModal();

        location.reload();

    } catch (error) {

        console.error(error);
        alert("An error occurred while adding the course.");

    }
}



//Edit modal
function openEditCourseModal(courseId, courseName) {

    document.getElementById("editCourseId").value = courseId;

    document.getElementById("editCourseName").value = courseName;

    document.getElementById("editCourseModal").style.display = "block";
}


function closeEditCourseModal() {

    document.getElementById("editCourseModal").style.display = "none";
}

//EditModalFunction
async function updateCourse() {

    const courseId =
        document.getElementById("editCourseId").value;

    const courseName =
        document.getElementById("editCourseName").value.trim();

    if (courseName === "") {
        alert("Please enter a course name.");
        return;
    }

    try {

        const response = await fetch(`/Courses/Edit/${courseId}`, {
            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
               CourseId: parseInt(courseId),
                CourseName: courseName
            })
        });

        if (!response.ok) {

            const errorMessage = await response.text();

            console.error("Server error:", errorMessage);

            alert("Failed to update course.");

            return;
        }

        closeEditCourseModal();

        location.reload();

    } catch (error) {

        console.error("Error:", error);

        alert("An error occurred while updating the course.");
    }
}



//detailsmodal
function openCourseDetailsModal(courseId, courseName) {

    document.getElementById("detailsCourseId").innerText =
        courseId;

    document.getElementById("detailsCourseName").innerText =
        courseName;

    document.getElementById("courseDetailsModal").style.display =
        "block";
}

//FunctionDetailsModal
function closeCourseDetailsModal() {

    document.getElementById("courseDetailsModal").style.display =
        "none";
} function openCourseDetailsModal(courseId, courseName) {

    document.getElementById("detailsCourseId").innerText =
        courseId;

    document.getElementById("detailsCourseName").innerText =
        courseName;

    document.getElementById("courseDetailsModal").style.display =
        "block";
}


function closeCourseDetailsModal() {

    document.getElementById("courseDetailsModal").style.display =
        "none";
}


//DeleteModal
function openDeleteCourseModal(courseId, courseName) {

    document.getElementById("deleteCourseId").value = courseId;

    document.getElementById("deleteCourseName").innerText =
        courseName;

    document.getElementById("deleteCourseModal").style.display =
        "block";
}

//deletefunction
function closeDeleteCourseModal() {

    document.getElementById("deleteCourseModal").style.display =
        "none";
}

async function deleteCourse() {

    const courseId =
        document.getElementById("deleteCourseId").value;

    try {

        const response = await fetch(
            `/Courses/DeleteCourse/${courseId}`,
            {
                method: "POST"
            }
        );

        if (!response.ok) {
            alert("Failed to delete course.");
            return;
        }

        closeDeleteCourseModal();

        location.reload();

    } catch (error) {

        console.error(error);

        alert("An error occurred while deleting the course.");
    }
}




// Department 
// Add Department Modal
function openAddDepartmentModal() {
    document.getElementById("addDepartmentModal").style.display = "block";
}

function closeAddDepartmentModal() {
    document.getElementById("addDepartmentModal").style.display = "none";
}


// Add for save button
async function addDepartment() {

    const departmentName =
        document.getElementById("departmentName").value.trim();

    if (departmentName === "") {
        alert("Please enter a department name.");
        return;
    }

    try {

        const response = await fetch("/Departments/Create", {
            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                departmentName: departmentName
            })
        });

        if (!response.ok) {
            alert("Failed to add department.");
            return;
        }

        closeAddDepartmentModal();

        location.reload();

    } catch (error) {

        console.error(error);
        alert("An error occurred while adding the department.");

    }
}

//Edit modal
function openEditDepartmentModal(DepartmentID, DepartmentName) {

    document.getElementById("editDepartmentId").value = DepartmentID;

    document.getElementById("editDepartmentName").value = DepartmentName;

    document.getElementById("editDepartmentModal").style.display = "block";
}


function closeEditDepartmentModal() {

    document.getElementById("editDepartmentModal").style.display = "none";
}

//EditModalFunction
async function updateDepartment() {

    const departmentID =
        document.getElementById("editDepartmentId").value;

    const departmentName =
        document.getElementById("editDepartmentName").value.trim();

    if (departmentName === "") {
        alert("Please enter a department name.");
        return;
    }

    try {

        const response = await fetch(`/Departments/Edit/${departmentID}`, {
            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                DepartmentId: parseInt(departmentID),
                DepartmentName: departmentName
            })
        });

        if (!response.ok) {

            const error = await response.text();

            console.error(error);

            alert("Failed to update department.");

            return;
        }

        closeEditDepartmentModal();

        location.reload();

    } catch (error) {

        console.error(error);

        alert("An error occurred while updating the department.");
    }
}


//detailsmodal
function openDepartmentDetailsModal(DepartmentId, DepartmentName) {

    document.getElementById("detailsDepartmentId").innerText =
        DepartmentId;

    document.getElementById("detailsDepartmentName").innerText =
        DepartmentName;

    document.getElementById("departmentDetailsModal").style.display =
        "block";
}

//FunctionDetailsModal
function closeDepartmentDetailsModal() {

    document.getElementById("departmentDetailsModal").style.display =
        "none";
}



//DeleteModal
function openDeleteDepartmentModal(departmentId, departmentName) {

    document.getElementById("deleteDepartmentId").value = departmentId;

    document.getElementById("deleteDepartmentName").innerText =
        departmentName;

    document.getElementById("deleteDepartmentModal").style.display =
        "block";
}

//deletefunction
function closeDeleteDepartmentModal() {

    document.getElementById("deleteDepartmentModal").style.display =
        "none";
}

async function deleteDepartment() {

    const departmentId =
        document.getElementById("deleteDepartmentId").value;

    try {

        const response = await fetch(
            `/Departments/DeleteDepartment/${departmentId}`,
            {
                method: "POST"
            }
        );

        if (!response.ok) {
            alert("Failed to delete department.");
            return;
        }

        closeDeleteDepartmentModal();

        location.reload();

    } catch (error) {

        console.error(error);

        alert("An error occurred while deleting the department.");
    }
}
