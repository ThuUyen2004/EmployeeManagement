$(function () {
   
    const main_content = $("#content-main");
    $("#employee-list-link").on("click", function () {
       
        $.ajax({
            url: '/Employee/EmployeeList',
            method: 'GET',
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        })
            .done(function (html) {
                main_content.html(html);
                
            })
            .fail(function (xhr) {
                main_content.html('<div class="alert alert-danger">' + (xhr.responseText || 'Không tải được chi tiết') + '</div>');
               
            });


    });
    $("#department-list-link").on("click", function () {

        $.ajax({
            url: '/Department/DepartmentList',
            method: 'GET',
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        })
            .done(function (html) {
                main_content.html(html);

            })
            .fail(function (xhr) {
                main_content.html('<div class="alert alert-danger">' + (xhr.responseText || 'Không tải được chi tiết') + '</div>');

            });




    });
    $("#jobposition-list-link").on("click", function () {

        $.ajax({
            url: '/JobPosition/JobpositionList',
            method: 'GET',
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        })
            .done(function (html) {
                main_content.html(html);

            })
            .fail(function (xhr) {
                main_content.html('<div class="alert alert-danger">' + (xhr.responseText || 'Không tải được chi tiết') + '</div>');

            });
        


    });
   
});

function getEmployeeByEmail(email) {
    alert(email);
    $.ajax({
        url: `api/employee/email/${email}`,
        method: 'GET'
       
    })
        .done(function (data) {
            console.log("Thành công:", data);
            alert(`Thành công! Tìm thấy ${data.data.length} nhân viên`);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Lỗi:", textStatus, errorThrown);
            alert(`Thất bại: ${textStatus}`);
        });
}