
export const CommonPages: any[] = [
    {
        pageTitle: "Profile",
        breadcrumbs: "Profile",
        pageName: "profile",
        leftSearchCriteria: "",
        rightView: "",
        routePath: "/app/profile",
    },
    {
        pageTitle: "Dashboard",
        breadcrumbs: "Dashboard",
        pageName: "dashboard",
        routePath: "/app/dashboard",
    }
];

export const RouteApiData: any[] = [
    {
        pageTitle: "Home",
        breadcrumbs: "Home",
        pageName: "home",
        leftSearchCriteria: "home-menu-tab/",
        rightView: "home-plot/",
        downloadAllReport: "download-all-reports",
        downloadReport: "download-portfolio-summary-report"
    }
]

export const MenuTabsData: any[] = [
    {
        "name": "Dashboard",
        "icon": "bi-house-door",
        "pageName": "dashboard",
        "url": "/app/dashboard",
        "subtabs": [],
        "enabled": true
    },
    {
        "name": "Master",
        "icon": "bi-bounding-box",
        "pageName": "master",
        "url": "/app/master",
        "subtabs": [
            {
                "name": "Script",
                "icon": "",
                "pageName": "script",
                "url": "/app/master-script",
                "subtabs": [],
                "enabled": true
            },
            {
                "name": "master",
                "icon": "",
                "pageName": "master",
                "url": "/app/master-master",
                "subtabs": [],
                "enabled": true
            },
           
        ],
        "enabled": true


    },
    {
        "name": "Account",
        "icon": "bi bi-person-badge-fill",
        "pageName": "account",
        "url": "/app/account",
        "subtabs": [
            {
                "name": "Menu",
                "icon": "",
                "pageName": "menu",
                "url": "/app/account-menu",
                "subtabs": [],
                "enabled": true
            },
            {
                "name": "Roles",
                "icon": "",
                "pageName": "role",
                "url": "/app/account-role",
                "subtabs": [],
                "enabled": true
            },
            {
                "name": "User",
                "icon": "",
                "pageName": "user",
                "url": "/app/account-user",
                "subtabs": [],
                "enabled": true
            }
        ],

        "enabled": true
    },
    
    
]
