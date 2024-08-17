import React from "react";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import {
  Trash2,
  Calendar,
  BarChart2,
  TrendingUp,
  TrendingDown,
} from "lucide-react";
import { useNavigate } from "react-router-dom";

const SalaryCard = ({ month, amount, transactions, growth, onDelete }) => {
  const navigate = useNavigate();

  const handleCardClick = () => {
    navigate(`/salary/2024-12/details`);
  };

  const TrendIcon = growth >= 0 ? TrendingUp : TrendingDown;

  return (
    <Card
      className="overflow-hidden transition-all duration-300 hover:shadow-lg cursor-pointer"
      onClick={handleCardClick}
    >
      <CardContent className="p-6">
        <div className="flex justify-between items-center mb-4">
          <div className="flex items-center space-x-2">
            <Calendar className="h-5 w-5" />
            <span className="font-medium text-foreground">{month}</span>
          </div>
          <Badge
            variant={growth >= 0 ? "default" : "destructive"}
            className="px-2 py-1"
          >
            <TrendIcon className={`h-3 w-3 mr-1 inline`} />
            {Math.abs(growth)}%
          </Badge>
        </div>

        <div className="flex items-center my-6">
          <div className="mr-4 text-5xl leading-none">¥</div>
          <div>
            <p className="text-sm text-muted-foreground mb-1">总收入</p>
            <p className="text-3xl leading-none">{amount}</p>
          </div>
        </div>

        <div className="flex justify-between items-center">
          <div className="flex items-center space-x-2">
            <BarChart2 className="h-5 w-5" />
            <p className="text-foreground">
              收入笔数: <span>{transactions}</span>
            </p>
          </div>
          <Button
            variant="ghost"
            size="sm"
            onClick={(e) => {
              e.stopPropagation();
              onDelete(month);
            }}
            className="hover:bg-secondary hover:text-secondary-foreground transition-colors"
          >
            <Trash2 className="h-4 w-4" />
          </Button>
        </div>
      </CardContent>
    </Card>
  );
};

export default SalaryCard;
