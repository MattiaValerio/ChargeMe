import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import {
  CarFront,
  LayoutDashboard,
  WalletMinimal,
  Map,
  LineChart,
  CircleUser,
  Menu,
  Search,
} from 'lucide-react';
import { Sheet, SheetTrigger, SheetContent } from '@/components/ui/sheet';
import { Button } from '../ui/button';
import { Input } from '@/components/ui/input';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';

interface LayoutProps {
  children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  const location = useLocation();

  const navLinks = [
    {
      path: '/dashboard',
      label: 'Dashboard',
      icon: <LayoutDashboard className="h-6 w-6" />,
    },
    { path: '/map', label: 'Map', icon: <Map className="h-6 w-6" /> },
    {
      path: '/vehicle',
      label: 'Vehicle',
      icon: <CarFront className="h-6 w-6" />,
    },
    {
      path: '/wallet',
      label: 'Wallet',
      icon: <WalletMinimal className="h-6 w-6" />,
    },
    {
      path: '/analytics',
      label: 'Analytics',
      icon: <LineChart className="h-6 w-6" />,
    },
  ];

  return (
    <React.Fragment>
      <div className="flex min-h-screen w-full flex-col">
        <header className="sticky top-0 flex h-16 items-center gap-4 border-b bg-background px-4 md:px-6">
          <nav className="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6">
            <Link
              className="flex items-center gap-2 text-lg font-semibold md:text-base"
              to="#">
              {navLinks.find((link) => link.path === location.pathname)
                ?.icon || <LayoutDashboard className="h-6 w-6" />}
              <span className="sr-only">Acme Inc</span>
            </Link>
            {navLinks.map((link) => (
              <Link
                key={link.path}
                to={link.path}
                className={
                  location.pathname === link.path
                    ? 'text-foreground'
                    : 'text-muted-foreground transition-colors hover:text-foreground'
                }>
                {link.label}
              </Link>
            ))}
          </nav>
          <Sheet>
            <SheetTrigger asChild>
              <Button
                variant="outline"
                size="icon"
                className="shrink-0 md:hidden">
                <Menu className="h-5 w-5" />
                <span className="sr-only">Toggle navigation menu</span>
              </Button>
            </SheetTrigger>
            <SheetContent side="left">
              <nav className="grid gap-6 text-lg font-medium">
                <Link
                  to="/"
                  className="flex items-center gap-2 text-lg font-semibold">
                  {navLinks.find((link) => link.path === location.pathname)
                    ?.icon || <LayoutDashboard className="h-6 w-6" />}
                  <span className="sr-only">Acme Inc</span>
                </Link>
                {navLinks.map((link) => (
                  <Link
                    key={link.path}
                    to={link.path}
                    className={
                      location.pathname === link.path
                        ? 'text-foreground'
                        : 'text-muted-foreground transition-colors hover:text-foreground'
                    }>
                    {link.label}
                  </Link>
                ))}
              </nav>
            </SheetContent>
          </Sheet>
          <div className="flex w-full items-center gap-4 md:ml-auto md:gap-2 lg:gap-4">
            <form className="ml-auto flex-1 sm:flex-initial">
              <div className="relative">
                <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
                <Input
                  type="search"
                  placeholder="Search products..."
                  className="pl-8 sm:w-[300px] md:w-[200px] lg:w-[300px]"
                />
              </div>
            </form>
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button
                  variant="secondary"
                  size="icon"
                  className="rounded-full">
                  <CircleUser className="h-5 w-5" />
                  <span className="sr-only">Toggle user menu</span>
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                <DropdownMenuLabel>My Account</DropdownMenuLabel>
                <DropdownMenuSeparator />
                <DropdownMenuItem>Settings</DropdownMenuItem>
                <DropdownMenuItem>Support</DropdownMenuItem>
                <DropdownMenuSeparator />
                <DropdownMenuItem>Logout</DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          </div>
        </header>
        <main className="flex min-h-[calc(100vh_-_theme(spacing.16))] flex-1 flex-col gap-4 bg-muted/40 p-4 md:gap-8 md:p-10">
          {children}
        </main>
      </div>
    </React.Fragment>
  );
};

export default Layout;
