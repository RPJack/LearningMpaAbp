﻿    var taskService = abp.services.app.taskAppServices;

(function(){
    var $taskStateCombobox = $("#TaskStateCombobox");
    $taskStateCombobox.change(function(){
        getTaskList();
    });
    var $modal = $(".modal");
    //显示modal时，光标显示在第一个输入框
    $modal.on('shown.bs.modal',function(){
        $modal.find('input:not([type=hidden]):first').focus();
    })
})(jQuery);
    
    //异步开始提交时，显示遮罩层
    function beginPost(modalId){
        var $modal = $(modalId);
        abp.ui.setBusy($modal);
    }

    //异步开始提交结束后，隐藏遮罩层并清空Form
    function hideForm(modalId){
        var $modal = $(modalId);

        var $form = $modal.find("form");
        abp.ui.clearBusy($modal);
        $modal.modal("hide");
        //创建成功后，要清空Form表单
        $form[0].reset();
    }

    //处理异步提交异常
    function errorPost(xhr,status,error,modalId){
        if(error.length>0){
            abp.notify.error("系统错误, 请稍后再试!");
            var $modal = $(modalId);
            abp.ui.clearBusy($modal);
        }
    }

    function editTask(id){
        abp.ajax({
            url:"/Tasks/Edit",
            data:{"id":id},
            type:"GET",
            dataType:"html"
        })
        .done(function (data){
            $("#edit").html(data);
            $("#editTask").modal("show");
        })
        .fail(function (data){
            abp.notify.error("系统错误！");
        });
}

    
function deleteTask(id) {

    abp.message.confirm(
        "是否删除Id为" + id + "的任务信息",
        function (isConfirmed) {
            if (isConfirmed) {
                taskService.deleteTask(id)
                    .done(function () {
                        abp.notify.info("删除任务成功！");
                        getTaskList();
                    });
            }
        }
    );

}

    function getTaskList() {
    var $taskStateCombobox = $('#TaskStateCombobox');
    var url = '/Tasks/GetList?state=' + $taskStateCombobox.val();
    abp.ajax({
        url: url,
        type: "GET",
        dataType: "html"
    })
        .done(function (data) {
            $("#taskList").html(data);
        });
}