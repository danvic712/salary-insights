// assets
import { Dashboard, Help, Key, SettingsSuggest, Api, ListAlt } from "@mui/icons-material";

// constant
const icons = { Dashboard, Help, Key, SettingsSuggest, Api, ListAlt };

console.log('------', import.meta.DEV);

const menuItems = {
    items: [
        {
            id: 'dashboard',
            title: 'Dashboard',
            type: 'group',
            children: [
                {
                    id: 'dashboard',
                    title: 'Dashboard',
                    type: 'item',
                    url: '/dashboard',
                    icon: icons.Dashboard,
                    breadcrumbs: false
                }
            ]
        },
        {
            id: 'data-center',
            title: 'Data Center',
            type: 'group',
            children: [{

                id: 'salary',
                title: 'Monthly Salary',
                type: 'item',
                url: '/salary',
                icon: icons.ListAlt,
                breadcrumbs: false
            }, {

                id: 'configuration',
                title: 'Configuration',
                type: 'item',
                url: '/configuration',
                icon: icons.SettingsSuggest,
                breadcrumbs: false
            }]
        },
        {
            id: 'links',
            title: 'Links',
            type: 'group',
            children: [
                {
                    id: 'swagger',
                    title: 'OpenAPI Documentation',
                    type: 'item',
                    url: import.meta.DEV ? 'https://localhost:5178' : '/swagger',
                    icon: icons.Api,
                    external: true,
                    target: true
                },
                {
                    id: 'documentation',
                    title: 'Wiki',
                    type: 'item',
                    url: 'https://github.com/danvic712/salary-insights',
                    icon: icons.Help,
                    external: true,
                    target: true
                }
            ]
        }
    ]
};

export default menuItems;