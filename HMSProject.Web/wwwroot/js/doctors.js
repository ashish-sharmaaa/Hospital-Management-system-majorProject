﻿jQuery.noConflict()(function ($) {
    $(document).ready(function () {
        loadDataTable();
    })
})

var dataTable;

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Doctors/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "25%" },
            { "data": "gender", "width": "15%" },
            { "data": "position", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Doctors/Detail/${data}" class="btn btn-primary" style="cursor:pointer; width:40px">
                                    <i class="fas fa-eye" style="color:white"></i>
                                </a>
                                &nbsp;
                                <a href="/Admin/Doctors/AddDoctor/${data}" class="btn btn-primary" style="cursor:pointer; width:40px">
                                    <i class="fas fa-edit" style="color:white"></i>
                                </a>
                                &nbsp;

                                //<a onclick=Delete("/Admin/Doctors/Delete/${data}") class="btn btn-danger" style="cursor:pointer; width:40px">
                                //    <i class="fas fa-trash-alt" style="color:white"></i>
                                //</a>
                            </div>`;
                },
                "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No Record Found!"
        }
    })
}

//function Delete(url) {
//    swal({
//        title: "ARE YOU SURE YOU WANT TO DELETE IT?",
//        text: "YOU WILL NOT BE ABLE TO RESTORE IT!",
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#BD6B55",
//        confirmButtonText: "YES, DELETE IT",
//        closeOnConfirm: true
//    },
//        function () {
//            $.ajax({
//                type: 'DELETE',
//                url: url,
//                success: function (data) {
//                    if (data.success) {
//                        toastr.success(data.message);
//                        dataTable.ajax.reload();
//                    }
//                    else {
//                        toastr.error(data.message);
//                    }
//                }
//            })
//        }
//    )
//}