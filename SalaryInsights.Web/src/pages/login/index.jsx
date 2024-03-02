import {
    Form,
    Checkbox,
    Button,
    Layout,
    Nav,
    Select,
    Typography,
    Card,
    Row,
    Col,
} from '@douyinfe/semi-ui';
import { IconSemiLogo } from '@douyinfe/semi-icons';
import { IconFaq, IconLocaleProvider } from '@douyinfe/semi-icons-lab';

export default function Login() {
    let year = new Date().getFullYear();

    const { Header, Content, Footer } = Layout;
    const { Text } = Typography;

    return (
        <div
            style={{
                display: 'flex',
                flexDirection: 'column',
                minHeight: '100vh',
                background: 'rgb(var(--semi-grey-0))',
            }}
        >
            <Header>
                <Nav
                    mode="horizontal"
                    header={{
                        logo: (
                            <IconSemiLogo
                                style={{ height: '36px', fontSize: 36 }}
                            />
                        ),
                        text: 'Salary Insights',
                    }}
                    footer={
                        <>
                            <Button
                                theme="borderless"
                                icon={<IconFaq size="large" />}
                                style={{
                                    color: 'var(--semi-color-text-2)',
                                    marginRight: '12px',
                                }}
                            />
                            <Select
                                defaultValue="Chinese"
                                style={{
                                    width: 120,
                                    color: 'var(--semi-color-text-2)',
                                    marginRight: '12px',
                                }}
                                insetLabel={<IconLocaleProvider />}
                            >
                                <Select.Option value="Chinese">
                                    中文
                                </Select.Option>
                                <Select.Option value="English">
                                    English
                                </Select.Option>
                            </Select>
                        </>
                    }
                ></Nav>
            </Header>
            <Content
                style={{
                    flex: 1,
                }}
            >
                <Row
                    type="flex"
                    justify="space-around"
                    align="middle"
                >
                    <Col span={10}>
                        <Card>11111</Card>
                    </Col>
                </Row>
            </Content>
            <Footer
                style={{
                    display: 'flex',
                    color: 'var(--semi-color-text-2)',
                    fontSize: '14px',
                    padding: '20px',
                    justifyContent: 'center',
                    flexShrink: 0,
                }}
            >
                <span>
                    Copyright © {year}{' '}
                    <Text
                        link={{
                            href: 'https://github.com/danvic712',
                            target: '_blank',
                        }}
                    >
                        Danvic Wang
                    </Text>{' '}
                    All Rights Reserved.{' '}
                </span>
            </Footer>
        </div>
    );
}
