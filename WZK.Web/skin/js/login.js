var ui={
	btnLogin:document.getElementById("btnLogin"),
	txtMobile:document.getElementById("txtMobile"),
	errMsg:document.getElementById("errMsg")
};
String.prototype.isMobile = function() {
	return /^(?:13\d|14\d|15\d|17\d|18\d)\d{5}(\d{3}|\*{3})$/.test(this);
};
String.prototype.Alltrim = function() {
	return this.replace(/\s/g, "");
};
ui.btnLogin.addEventListener('tap',function(){
	var mobile=ui.txtMobile.value.Alltrim();
	if(mobile==''){
		ui.errMsg.innerText='请输入手机号码';
		return;
	}else if(!mobile.isMobile()){
		ui.errMsg.innerText='手机号码格式不正确';
		return;
	}
	ui.btnLogin.innerText='登录中，请稍候...';
	ui.btnLogin.setAttribute('disabled','disabled');
	mui.ajax('/Passport/Login', {
		type:"post",
		timeout:20000,
		data: {Mobile:mobile},
		success: function(data) {
			var IsPass=data.IsPass;
			if(IsPass){
			    ui.btnLogin.innerText = '登录';
			    ui.btnLogin.removeAttribute('disabled');
			    location.href = '/Home/Index';
			}else{
				ui.errMsg.innerText=data.Desc;
				ui.btnLogin.innerText='登录';
		        ui.btnLogin.removeAttribute('disabled');
			}
		},
		error: function(xhr, type, errorThrown) {
			ui.errMsg.innerText='无法连接到服务器，请检查网络设置';
			ui.btnLogin.innerText='登录';
	        ui.btnLogin.removeAttribute('disabled');
		}
	});
});