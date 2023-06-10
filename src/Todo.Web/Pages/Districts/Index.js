$(function () {
    var l = abp.localization.getResource('Todo');
    var createModal = new abp.ModalManager(abp.appPath + 'Districts/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Districts/EditModal');
    var deletedModal = new abp.ModalManager(abp.appPath + 'Districts/DeletedModal');

    var dataTable = $('#DistrictsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(todo.districts.district.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('Todo.Districts.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Todo.Districts.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DistrictDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.districts.district
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
                    title: l('OrderBy'),
                    data: "orderBy"
                },
            ]
        })
    );
    var dataTableDeleted = $('#DistrictsDeletedTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(todo.districts.district.getListDeleted),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Restore'),
                                    visible: abp.auth.isGranted('Todo.Districts.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DistrictRestoreConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.districts.district
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
                                    visible: abp.auth.isGranted('Todo.Districts.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DistrictDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.districts.district
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

    $('#NewDistrictButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});