$(function () {
    var l = abp.localization.getResource('Todo');
    var createModal = new abp.ModalManager(abp.appPath + 'Communes/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Communes/EditModal');
    var deletedModal = new abp.ModalManager(abp.appPath + 'Communes/DeletedModal');

    var dataTable = $('#CommunesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(todo.communes.commune.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('Todo.Communes.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Todo.Communes.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'CommuneDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.communes.commune
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Code'),
                    data: "code"
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('Province'),
                    data: "provinceName"
                },
                {
                    title: l('District'),
                    data: "districtName"
                },
                {
                    title: l('OrderBy'),
                    data: "orderBy"
                },
            ]
        })
    );
    var dataTableDeleted = $('#CommunesDeletedTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(todo.communes.commune.getListDeleted),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Restore'),
                                    visible: abp.auth.isGranted('Todo.Communes.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'CommuneRestoreConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.communes.commune
                                            .restore(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyRestore')
                                                );
                                                dataTableDeleted.ajax.reload();
                                            });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Todo.Communes.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'CommuneDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.communes.commune
                                            .confirmDelete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTableDeleted.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Code'),
                    data: "code"
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('Province'),
                    data: "provinceName"
                },
                {
                    title: l('District'),
                    data: "districtName"
                },
                {
                    title: l('OrderBy'),
                    data: "orderBy"
                },
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCommuneButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
//$(document).ready(function () {
//    // Bind change event to Province select element
//    $("#Commune_ProvinceId").change(function () {
//        var selectedProvinceId = $(this).val();
//        $.ajax({
//            url: "/Communes/CreateModal?handler=ProvinceSelected",
//            type: "GET",
//            data: { provinceId: selectedProvinceId },
//            success: function (data) {
//                // Replace District select element with new options
//                $("#Commune_DistrictId").html(data);
//            }
//        });
//    });
//});