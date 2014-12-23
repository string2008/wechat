<?php
/**
 *	微信公众平台企业号验证SDK
 *  @QQ  94560716
 *  @link https://github.com/string2008/wechat
 *  @version 1.0
 *  usage:
 *   	
 *
 */

include_once "qyhwechat.class.php";
include_once "qyhmessage.class.php";

//option内面的内容必须替换成你自己企业号的相关信息
//如有问题，请联系qq:94560716
$options = array(
        'token'=>'mytoken',						//填写应用接口的Token
        'encodingaeskey'=>'pGzDvv3aYxUGHKLJGFHHGGFFGTIK4DVzIh5e4kNB',		//填写加密用的EncodingAESKey
		'corpid'=>'wx111111111111111111',		//填写高级调用功能的corpid
		'secret'=>'0W-l6IFKrCi9634240ceLqXYrrFwrYFX12qPiWHGqAlVm5sSlI7DO1sdffhgytregf',	//填写高级调用功能的secret,在管理组中有相关信息，这个必须修改
		'agentid'=>'3',	
		);

$wechatObj = new wechatCallbackapiIMP();
$wechatObj->responseMsg($options);

class wechatCallbackapiIMP
{
	public function responseMsg($options)
    {
		$method=$_SERVER['REQUEST_METHOD'];
		if($method=='GET')
		{
			$qyhWechat = new QYHWechat($options);
			echo $qyhWechat->valid();
		}
		else 
		{
			$postStr = $GLOBALS["HTTP_RAW_POST_DATA"];
			$qyhMessage = new QYHMessage($postStr,$options);
		}

		exit;
		
    }
}

?>