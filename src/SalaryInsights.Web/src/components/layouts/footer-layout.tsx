import { cn } from "@/lib/utils";

interface FooterLayoutProps {
  leftContent: React.ReactNode;
  rightContent: React.ReactNode;
  className?: string;
}

const FooterLayout: React.FC<FooterLayoutProps> = ({
  leftContent,
  rightContent,
  className = "",
}) => {
  return (
    <div className={cn("flex items-center justify-between w-full", className)}>
      <div className="flex-1 text-sm text-muted-foreground">{leftContent}</div>
      <div className="flex items-center space-x-6 lg:space-x-8">
        {rightContent}
      </div>
    </div>
  );
};

export default FooterLayout;
