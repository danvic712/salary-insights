import { Outlet } from 'react-router-dom';

import { Layout, BackTop } from '@douyinfe/semi-ui';

import Header from './header';
import Footer from './footer';

import * as styles from './index.scss';

export default function MainLayout() {
    const { Content } = Layout;

    const style = {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        height: 30,
        width: 30,
        borderRadius: '100%',
        backgroundColor: '#0077fa',
        color: '#fff',
        bottom: 100,
    };

    return (
        <Layout className={styles.rootSidenavTab}>
            <Header />
            <Content
                style={{
                    padding: '20px',
                    backgroundColor: 'var(--semi-color-bg-1)',
                }}
            >
                <div
                    style={{
                        borderRadius: '10px',
                        border: '1px solid var(--semi-color-border)',
                        height: '125vh',
                        padding: '0px 20px',
                    }}
                >
                    <Outlet />
                </div>
            </Content>
            <BackTop style={style} />
            <Footer />
        </Layout>
    );
}
