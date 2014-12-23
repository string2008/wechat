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
//http://www.wechatee.com/github/ 可以用这个url及下面的code测试通过没有问题再贴换成您自己的信息
$options = array(
        'token'=>'94jwmLl8UvMf',						//填写应用接口的Token
        'encodingaeskey'=>'MdL9cqieVM6okw9GsDr5NASsOm38oiMORzjDsWTH3NG',		//填写加密用的EncodingAESKey
		'corpid'=>'wx06265c957d2f66ff',		//填写高级调用功能的corpid
		'secret'=>'FGrMDLKarmAb1eDl44Mf99Z6pgYbidtt1Y6FnW_4SEcZVX52A_0khZYkK6UFEaQK',	//填写高级调用功能的secret,在管理组中有相关信息，这个必须修改
		'agentid'=>'1',	
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