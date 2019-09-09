﻿using io.rong.methods.user.blacklist;
using io.rong.models;
using io.rong.models.response;
using io.rong.models.push;
using System;

namespace io.rong.example.user
{
    public class BlackListExample

    {
        /**
        * 此处替换成您的appKey
        * */
        private static readonly string appKey = "pwe86ga5pwrj6";
        /**
         * 此处替换成您的appSecret
         * */
        private static readonly string appSecret = "rb8fWki1mJcK";
        /**
         * 自定义api地址
         * */
        private static readonly string api = "http://api.cn.ronghub.com";

        static void Main(string[] args)
        {

            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api 地址方式
            // RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            Blacklist blackList = rongCloud.User.blackList;

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/user/black.html#add
             * 添加用户到黑名单方法
             */
            UserModel blackUser = new UserModel() { Id = "hdsjGB88" };
            UserModel[] blacklist = { blackUser };
            UserModel user = new UserModel()
            {
                Id = "hdsjGB89",
                Blacklist = blacklist
            }
;


            Result userAddBlacklistResult = blackList.Add(user);
            Console.WriteLine("addBlacklist:  " + userAddBlacklistResult);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/user/black.html#getList
             * 获取某用户的黑名单列表方法
             */
            UserModel user2 = new UserModel() { Id = "hdsjGB89" };

            BlackListResult result = blackList.GetList(user2);
            Console.WriteLine("query blacklist:  " + result);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/user/black.html#remove
             * 从黑名单中移除用户方法
             */
            Result removeResult = blackList.Remove(user);
            Console.WriteLine("remove blacklist:  " + removeResult);

            Console.ReadLine();

        }
    }
}
